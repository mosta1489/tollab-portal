using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;

namespace TolabPortal.DataAccess.Services
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> StudentLogin(string loginPhone, string email);

        Task<HttpResponseMessage> VerifyStudentLogin(string phoneKey, string phone, string verificationCode, string password);

        Task<HttpResponseMessage> RegisterStudent(Student student);

        Task<HttpResponseMessage> GetStudentProfile();

        Task<HttpResponseMessage> StudentCredentialsLogin(string userName, string password, bool rememberMe);

        Task<HttpResponseMessage> UpdateStudentProfile(Student student);

        Task<HttpResponseMessage> LogoutStudent();
        Task<HttpResponseMessage> ChangeStudentProfilePhoto(Student student);
        Task<HttpResponseMessage> GetStudentById(string userId);
    }

    public class AccountService : IAccountService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionManager _sessionManager;

        public AccountService(IOptions<ApplicationConfig> options,
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

        public async Task<HttpResponseMessage> StudentLogin(string loginPhone, string email)
        {
            try
            {
                var studentLoginResponse = await _httpClient.GetAsync($"/api/login-web?PhoneNumberWithKey={loginPhone}&email={email}");
                return studentLoginResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetStudentById(string userId)
        {
            try
            {
                var studentLoginResponse = await _httpClient.GetAsync($"/api/get-student-by-Id/{userId}");
                return studentLoginResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        

        public async Task<HttpResponseMessage> VerifyStudentLogin(string phoneKey, string phone, string verificationCode, string password)
        {
            try
            {
                var studentLoginVerificationResponse = await _httpClient.GetAsync($"/api/VerifyWeb?PhoneKey={phoneKey}&Phone={phone}&vcode={verificationCode}&password={password}");
                return studentLoginVerificationResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> RegisterStudent(Student student)
        {
            try
            {
                var studentJson = JsonConvert.SerializeObject(student);
                var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

                var studentRegisterResponse = await _httpClient.PostAsync($"/api/Register", content);
                return studentRegisterResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetStudentProfile()
        {
            try
            {
                var studentProfileResponse = await _httpClient.GetAsync($"/api/GetStudentProfile");
                return studentProfileResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> StudentCredentialsLogin(string userName, string password, bool rememberMe)
        {
            try
            {
                return await _httpClient.PostAsync("api/students/credentials/login", new StringContent(JsonConvert.SerializeObject(new { userName, password, rememberMe }), Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> LogoutStudent()
        {
            try
            {
                var studentProfileResponse = await _httpClient.GetAsync($"/api/StudentLogout");
                return studentProfileResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> UpdateStudentProfile(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException();

                var sectionsResponse = await _httpClient.PostAsync("/api/EditProfile", new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json"));
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> ChangeStudentProfilePhoto(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException();

                var sectionsResponse = await _httpClient.PostAsync("/api/ChangePhoto", new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json"));
                return sectionsResponse;
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