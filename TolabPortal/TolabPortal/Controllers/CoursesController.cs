using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;

namespace TolabPortal.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISessionManager _sessionManager;
        private readonly IAccountService _accountService;
        private readonly IInterestService _interestService;

        public CoursesController(ICourseService courseService, ISessionManager sessionManager, IAccountService accountService, IInterestService interestService)
        {
            _courseService = courseService;
            _sessionManager = sessionManager;
            _accountService = accountService;
            _interestService = interestService;
        }

        public async Task<IActionResult> Index()
        {
            var studentProfileResponse = await _accountService.GetStudentProfile();
            var student = await CommonUtilities.GetResponseModelFromJson<GetStudentProfileModel>(studentProfileResponse);

            List<long> departmentsIds = new List<long>();

            var interest = student.model.Interests.FirstOrDefault();
            ViewBag.Interest = interest;
            foreach (var modelInterest in student.model.Interests)
            {
                var subCategoryId = modelInterest.SubCategoryId;
                var departmentResponse = await _interestService.GetDepartmentsBySubCategoryId(subCategoryId);

                var department = await CommonUtilities.GetResponseModelFromJson<DepartmentResponse>(departmentResponse);
                departmentsIds.AddRange(department.Departments.Select(d => d.Id));
            }

            List<SubjectResponse> subjects = new List<SubjectResponse>();
            foreach (var departmentsId in departmentsIds)
            {
                var subjectsResponse = await _courseService.GetSubjectsWithTracksByDepartmentId(departmentsId);
                if (subjectsResponse.IsSuccessStatusCode)
                {
                    var subject = await CommonUtilities.GetResponseModelFromJson<SubjectResponse>(subjectsResponse);
                    subjects.Add(subject);
                }
            }

            return View("Index", subjects.SelectMany(s => s.Subjects).SelectMany(s => s.Tracks));
        }

        [Route("HomeCourses")]
        public async Task<IActionResult> GetHomeCourses()
        {
            var response = await _courseService.GetHomeCourses();
            if (response.IsSuccessStatusCode)
            {
                var courses = await CommonUtilities.GetResponseModelFromJson<StudentHomeCourseResponse>(response);
                return View("Index", courses.StudentHomeCourses);
            }
            else
            {
            }
            return View("Index");
        }

        [Route("SubjectsWithTracks")]
        public async Task<IActionResult> GetSubjectsWithTracksByDepartmentId()
        {
            long sampleDepartmentId = 10028;
            var response = await _courseService.GetSubjectsWithTracksByDepartmentId(sampleDepartmentId);
            if (response.IsSuccessStatusCode)
            {
                var subjects = await CommonUtilities.GetResponseModelFromJson<SubjectResponse>(response);
                return View("Index", subjects.Subjects);
            }
            else
            {
            }
            return View("Index");
        }
    }
}