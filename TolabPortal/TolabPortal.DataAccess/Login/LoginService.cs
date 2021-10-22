using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Login.Models;

namespace TolabPortal.DataAccess.Login
{
    public interface ILoginService
    {
        Task<HttpResponseMessage> StudentLogin(string loginPhone);
    }

    public class LoginService : ILoginService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public LoginService(IOptions<ApplicationConfig> options)
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
            var studentLoginResponse = await _httpClient.GetAsync($"/api/StudentLogin?{loginPhone}");

            if (studentLoginResponse.IsSuccessStatusCode)
            {
                var responseString = await studentLoginResponse.Content.ReadAsStringAsync();
                var studentLoginResult = JsonConvert.DeserializeObject<Student>(responseString);


            }

            throw new NotImplementedException();
        }
    }
}