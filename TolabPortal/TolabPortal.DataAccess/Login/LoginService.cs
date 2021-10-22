using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using Tolab.Common;

namespace TolabPortal.DataAccess.Login
{
    public interface ILoginService
    {
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
    }
}