using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tolab.Common;

namespace TolabPortal.DataAccess.Services
{
    public interface ISubscribeService
    {
       
        Task<HttpResponseMessage> SubscribeCourse(long courseId, string promoCode = "");
        Task<HttpResponseMessage> SubscribeLive(long liveId, string promoCode = "");
        Task<HttpResponseMessage> SubscribeTrack(long trackId, string promoCode = "");
    }

    public class SubscribeService : IDisposable, ISubscribeService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionManager _sessionManager;

        public SubscribeService(IOptions<ApplicationConfig> options,
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

        public async Task<HttpResponseMessage> SubscribeCourse(long courseId, string promoCode = "")
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/BuyNow?CourseId={courseId}&PromocodeText={promoCode}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> SubscribeLive(long liveId, string promoCode = "")
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/BuyNow?liveId={liveId}&PromocodeText={promoCode}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> SubscribeTrack(long trackId, string promoCode = "")
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/TrackSubscription?TrackId={trackId}&PromocodeText={promoCode}");
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
