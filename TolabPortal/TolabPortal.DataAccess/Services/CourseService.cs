using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Models.Exams;

namespace TolabPortal.DataAccess.Services
{
    public interface ICourseService
    {
        Task<HttpResponseMessage> GetCoursesByDepartmentId(long departmentId, int page = 0);
        Task<HttpResponseMessage> GetSubjectsWithTracksByDepartmentId(long departmentId, int page = 0);
        Task<HttpResponseMessage> GetHomeCourses(int page = 0);

        Task<HttpResponseMessage> GetTopLives();
        Task<HttpResponseMessage> GetLives(int page = 0);
        Task<HttpResponseMessage> GetLiveDetails(int liveId);
        Task<HttpResponseMessage> GetStudentLives(long studentId, int page = 0);

        Task<HttpResponseMessage> GetTrackById(long trackId);
        Task<HttpResponseMessage> GetCourseWithOneContentForStudent(long courseId, long contentId, long videoQuestionId);
        Task<HttpResponseMessage> GetCoursesByTrackId(int trackId);
        Task<HttpResponseMessage> GetCourseByIdForCurrentStudent(long courseId);
        Task<HttpResponseMessage> GetStudentCourses(int page = 0);
        Task<HttpResponseMessage> GetGroupsWithContentsByCourseIdForCurrentStudent(long courseId, int page = 0, long contentId = 0);
        Task<HttpResponseMessage> ViewThisContent(long contentId);

        Task<HttpResponseMessage> GetQuestions(long courseId, int page = 0, long videoQuestionId = 0);
        Task<HttpResponseMessage> AddQuestion(string comment, float minute, string image, long contentId, long liveId, bool viewMyAccount);
        Task<HttpResponseMessage> AddStudentReply(string comment, long videoQuestionId, bool viewMyAccount, string image);
        Task<HttpResponseMessage> GetStudentExams(long? courseId = null, long? solveStatusId = null, int page = 0);
        Task<HttpResponseMessage> GetTeacherById(long teacherId);
    }
    public class CourseService : ICourseService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionManager _sessionManager;

        public CourseService(IOptions<ApplicationConfig> options,
            ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            var config = options.Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(config.ApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_sessionManager.AccessToken}");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        #region Lives
        public async Task<HttpResponseMessage> GetTopLives()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetTopLives");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetLives(int page = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetLives?Page={page}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetLiveDetails(int liveId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetLiveDetails?id={liveId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetStudentLives(long studentId, int page = 0)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetStudentLives?studentId={studentId}&page={page}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        #endregion

        #region Subjects, Tracks and Courses Services
        public async Task<HttpResponseMessage> GetCoursesByDepartmentId(long departmentId, int page = 0)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetCoursesByDepartmentId?departmentId={departmentId}&Page={page}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetSubjectsWithTracksByDepartmentId(long departmentId, int page = 0)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSubjectsWithTracksByDepartmentId?departmentId={departmentId}&Page={page}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetHomeCourses(int page = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetHomeCourses?Page={page}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetTrackById(long trackId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetTrackById?TrackId={trackId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetCourseWithOneContentForStudent(long courseId, long contentId, long videoQuestionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetCourseWithOneContentForStudent?CourseId={courseId}&ContentId={contentId}&VideoQuestionId={videoQuestionId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetCoursesByTrackId(int trackId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetCoursesByTrackId?TrackId={trackId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetCourseByIdForCurrentStudent(long courseId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetCourseByIdForCurrentStudent?CourseId={courseId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> GetStudentCourses(int page = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetStudentCourses?Page={page}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        #endregion

        public async Task<HttpResponseMessage> GetGroupsWithContentsByCourseIdForCurrentStudent(long courseId, int page, long contentId = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetGroupsWithContentsByCourseIdForCurrentStudent?CourseId={courseId}&Page={page}&ContentId={contentId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> ViewThisContent(long contentId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ViewThisContent?ContentId={contentId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        #region Course Questions
        public async Task<HttpResponseMessage> GetQuestions(long courseId, int page = 0, long videoQuestionId = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetQuestions?CourseId={courseId}&Page={page}&VideoQuestionId={videoQuestionId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> AddQuestion(string question, float minute, string image, long contentId, long liveId, bool viewMyAccount)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Question", question),
                    new KeyValuePair<string, string>("Minute", minute.ToString()),
                    new KeyValuePair<string, string>("Image", image),
                    new KeyValuePair<string, string>("ContentId", contentId.ToString()),
                    new KeyValuePair<string, string>("LiveId", liveId.ToString()),
                    new KeyValuePair<string, string>("ViewMyAccount", viewMyAccount.ToString())
                });

                var response = await _httpClient.PostAsync("api/AddQuestion", formContent);
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> AddStudentReply(string comment, long videoQuestionId, bool viewMyAccount, string image)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Comment", comment),
                    new KeyValuePair<string, string>("VideoQuestionId", videoQuestionId.ToString()),
                    new KeyValuePair<string, string>("ViewMyAccount", viewMyAccount.ToString()),
                    new KeyValuePair<string, string>("Image", image),
                });

                var response = await _httpClient.PostAsync("api/AddStudentReply", formContent);
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message
                };
                return errorResponse;
            }
        }
        #endregion

        #region Course Exams
        public async Task<HttpResponseMessage> GetStudentExams(long? courseId = null, long? solveStatusId = null, int page = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetStudentExams?CourseId={courseId}&SolveStatusId={solveStatusId}&Page={page}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetExamDeatailsForStudent(long examId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetExamDeatailsForStudent?ExamId={examId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> StartPdfExam(long examId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/StartPdfExam?ExamId={examId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> AnswerPdfExam(long examQuestionId, long studentExamId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
               {
                    new KeyValuePair<string, string>("ExamQuestionId", examQuestionId.ToString()),
                    new KeyValuePair<string, string>("StudentExamId", studentExamId.ToString()),
                });

                var response = await _httpClient.PostAsync("api/AnswerPdfExam", formContent);
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> StartExam(long examId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/StartExam?ExamId={examId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> AnswerExam(StudentAnswersWithStudentExamId answersWithStudentExamId)
        {
            try
            {
                var json = JsonConvert.SerializeObject(answersWithStudentExamId);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/AnswerExam", content);
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        #endregion

        public async Task<HttpResponseMessage> GetTeacherById(long teacherId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetTeacherById?TeacherId={teacherId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

    }

}
