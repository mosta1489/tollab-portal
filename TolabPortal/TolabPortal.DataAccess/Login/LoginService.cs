using System;
using System.Net.Http;

namespace TolabPortal.DataAccess.Login
{
    public interface ILoginService
    {
    }

    public class LoginService : ILoginService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public LoginService()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}