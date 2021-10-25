using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TolabPortal.DataAccess.Services.Payment
{
    public interface IMyFatoorahClient
    {
        public Task<string> PerformRequest(string requestJSON, string url = "", string endPoint = "");
        public Task<string> PerformRequest(string requestUrl);

    }
    public class MyFatoorahClient : IMyFatoorahClient
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClient;

        private string _apiKey;
        private string _apiUrl;
        public MyFatoorahClient(IConfiguration config, IHttpClientFactory httpClient)
        {
            _config = config;
            _apiKey = _config[$"MyFatoorahApiKey"];
            _apiUrl = _config[$"MyFatooraApiUrl"];
            _httpClient = httpClient;
        }
        public async Task<string> PerformRequest(string requestJSON, string url = "", string endPoint = "")
        {
            if (string.IsNullOrEmpty(url))
                url = _apiUrl + $"/v2/{endPoint}";
            var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var responseMessage = await client.PostAsync(url, httpContent).ConfigureAwait(false);
            string response = string.Empty;
            if (!responseMessage.IsSuccessStatusCode)
            {
                response = JsonConvert.SerializeObject(new
                {
                    IsSuccess = false,
                    Message = responseMessage.StatusCode.ToString()
                });
            }
            else
            {
                response = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }
        public async Task<string> PerformRequest(string requestUrl)
        {
            string url = _apiUrl + requestUrl;
            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var responseMessage = await client.PostAsync(url, null).ConfigureAwait(false);
            string response = string.Empty;
            if (!responseMessage.IsSuccessStatusCode)
            {
                response = JsonConvert.SerializeObject(new
                {
                    IsSuccess = false,
                    Message = responseMessage.StatusCode.ToString()
                });
            }
            else
            {
                response = await responseMessage.Content.ReadAsStringAsync();
            }

            return response;
        }

    }
}
