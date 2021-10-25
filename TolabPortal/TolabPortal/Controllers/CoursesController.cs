using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;

namespace TolabPortal.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISessionManager _sessionManager;

        public CoursesController(ICourseService courseService,
           ISessionManager sessionManager)
        {
            _courseService = courseService;
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            return View();
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