using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tolab.Common;
using System.Net;

namespace TolabPortal.DataAccess.Services
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> StudentLogin(string loginPhone);
        Task<HttpResponseMessage> VerifyStudentLogin(string phoneKey, string phone, string verificationCode);
    }

    public class AccountService : IAccountService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public AccountService(IOptions<ApplicationConfig> options)
        {
            var config = options.Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(config.ApiUrl) };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        public async Task<HttpResponseMessage> StudentLogin(string loginPhone)
        {
            try
            {
                var studentLoginResponse = await _httpClient.GetAsync($"/api/StudentLogin?PhoneNumberWithKey={loginPhone}");
                return studentLoginResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> VerifyStudentLogin(string phoneKey, string phone, string verificationCode)
        {
            try
            {
                var studentLoginVerificationResponse = await _httpClient.GetAsync($"/api/Verify?PhoneKey={phoneKey}&Phone={phone}&vcode={verificationCode}");
                return studentLoginVerificationResponse;
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