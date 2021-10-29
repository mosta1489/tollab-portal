using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Models.Exams;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;

namespace TolabPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SubjectsController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISessionManager _sessionManager;
        private readonly IAccountService _accountService;
        private readonly IInterestService _interestService;
        private readonly ISubscribeService _subscribeService;

        public SubjectsController(ICourseService courseService, ISessionManager sessionManager, IAccountService accountService,
            IInterestService interestService, ISubscribeService subscribeService)
        {
            _courseService = courseService;
            _sessionManager = sessionManager;
            _accountService = accountService;
            _interestService = interestService;
            _subscribeService = subscribeService;
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

        [Route("Track")]
        public async Task<IActionResult> GetTrackCourses(int trackId)
        {
            var trackDetailsResponse = await _courseService.GetCoursesByTrackId(trackId);
            if (trackDetailsResponse.IsSuccessStatusCode)
            {
                var trackDetails = await CommonUtilities.GetResponseModelFromJson<CoursesByTrackIdModelResponse>(trackDetailsResponse);

                // getting student interest to show in interests path
                var studentProfileResponse = await _accountService.GetStudentProfile();
                var student = await CommonUtilities.GetResponseModelFromJson<GetStudentProfileModel>(studentProfileResponse);
                var interest = student.model.Interests.FirstOrDefault();
                ViewBag.Interest = interest;

                // getting teacher image
                var teacherProfileResponse = await _courseService.GetTeacherById(trackDetails.CoursesByTrackId.Courses.FirstOrDefault().TeacherId);
                if (teacherProfileResponse.IsSuccessStatusCode)
                {
                    var teacherProfile = await CommonUtilities.GetResponseModelFromJson<TeacherResponse>(teacherProfileResponse);
                    trackDetails.CoursesByTrackId.TeacherPhoto = teacherProfile.Teacher.Photo;
                }

                // getting user transactions / subscribtions to check whether it contains the id of each course in this track
                var studentTransactionsResponse = await _subscribeService.GetAllStudentTransactions();
                if (studentTransactionsResponse.IsSuccessStatusCode)
                {
                    var studentTransactions = await CommonUtilities.GetResponseModelFromJson<StudentTransactionsResponse>(studentTransactionsResponse);

                    // update which course are paid for current student
                    foreach (var course in trackDetails.CoursesByTrackId.Courses)
                    {
                        course.IsCurrentStudentSubscribedToCourse = studentTransactions.studentTransactionsVM.studentTransactions.Any(t => t.CourseId == course.Id);
                    }
                    trackDetails.CoursesByTrackId.Courses.ToList()[0].IsCurrentStudentSubscribedToCourse = true;
                }
                return View("TrackDetails", trackDetails.CoursesByTrackId);
            }
            else
            {
                return View("TrackDetails", new CoursesByTrackIdModel());
            }
        }

        [Route("Track/Course")]
        public async Task<IActionResult> GetCourseDetails(long courseId, string trackNameLT)
        {
            var courseDetailsResponse = await _courseService.GetCourseByIdForCurrentStudent(courseId);
            if (courseDetailsResponse.IsSuccessStatusCode)
            {
                var courseDetails = await CommonUtilities.GetResponseModelFromJson<CourseResponse>(courseDetailsResponse);

                // getting student interest to show in interests path
                var studentProfileResponse = await _accountService.GetStudentProfile();
                var student = await CommonUtilities.GetResponseModelFromJson<GetStudentProfileModel>(studentProfileResponse);
                var interest = student.model.Interests.FirstOrDefault();

                DataAccess.Models.ItemDetails abstractCourseInfo = new DataAccess.Models.ItemDetails();
                abstractCourseInfo.PageRootName = "الرئيسية";
                abstractCourseInfo.SectionName = interest.SectionNameLT;
                abstractCourseInfo.CategoryName = interest.CategoryNameLT;
                abstractCourseInfo.SubCategoryName = interest.SubCategoryNameLT;

                abstractCourseInfo.TrackName = trackNameLT;
                abstractCourseInfo.CourseName = courseDetails.Course.NameLT;
                abstractCourseInfo.TeacherName = courseDetails.Course.TeacherName;

                // getting user transactions / subscribtions to check whether it contains the id of current course
                var studentTransactionsResponse = await _subscribeService.GetAllStudentTransactions();
                if (studentTransactionsResponse.IsSuccessStatusCode)
                {
                    var studentTransactions = await CommonUtilities.GetResponseModelFromJson<StudentTransactionsResponse>(studentTransactionsResponse);
                    courseDetails.Course.IsCurrentStudentSubscribedToCourse = studentTransactions.studentTransactionsVM.studentTransactions.Any(t => t.CourseId == courseDetails.Course.Id);
                }

                // abstract course info is being used by details layout
                courseDetails.Course.ItemDetails = abstractCourseInfo;

                // getting Course Content
                var groupsWithContentsResponse = await _courseService.GetGroupsWithContentsByCourseIdForCurrentStudent(courseId);
                if (groupsWithContentsResponse.IsSuccessStatusCode)
                {
                    var groupsWithContents = await CommonUtilities.GetResponseModelFromJson<GroupResponse>(groupsWithContentsResponse);
                    courseDetails.Course.Groups = groupsWithContents.Groups;
                }

                // getting Course Questions
                var videoQuestionsResponse = await _courseService.GetQuestions(courseId);
                if (videoQuestionsResponse.IsSuccessStatusCode)
                {
                    var videoQuestions = await CommonUtilities.GetResponseModelFromJson<VideoQuestionResponse>(videoQuestionsResponse);
                    courseDetails.Course.VideoQuestions = videoQuestions.VideoQuestions;
                }
                courseDetails.Course.IsCurrentStudentSubscribedToCourse = true;

                // getting Course Exams
                //var examsResponse = await _courseService.GetStudentExams(courseId);
                //if (examsResponse.IsSuccessStatusCode)
                //{
                //    var exams = await CommonUtilities.GetResponseModelFromJson<StudentExamsToCorrectResponse>(examsResponse);
                //    courseDetails.Course.StudentExams = exams.StudentExamsToCorrect;

                //}

                return View("CourseDetails", courseDetails.Course);
            }
            else
            {
                return View("CourseDetails", new Course());
            }
        }

        //[Route("Track/Course/VideQuestion")]
        //public async Task<IActionResult> AddCourseVideoQuestion()
        //{
        //}
    }
}