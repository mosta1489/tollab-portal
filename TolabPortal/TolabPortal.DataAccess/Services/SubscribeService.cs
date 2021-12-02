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

        Task<HttpResponseMessage> SubscribeCourse(string message, long courseId, string promoCode = "",string userId="", bool isWebHook = false);
        Task<HttpResponseMessage> SubscribeLive(string message, long liveId, string promoCode = "", string userId = "", bool isWebHook = false);
        Task<HttpResponseMessage> SubscribeTrack(long trackId, string promoCode = "", string userId = "", bool isWebHook = false);
        Task<HttpResponseMessage> GetAllStudentTransactions(int page = 0);
        Task<HttpResponseMessage> InsertInvoiceLog(long invoiceId);
        Task<HttpResponseMessage> UpdateInvoiceLog(long invoiceId);
        Task<HttpResponseMessage> FixFailedInvoices();
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

        public async Task<HttpResponseMessage> SubscribeCourse(string message, long courseId, string promoCode = "", string userId = "",bool isWebHook=false)
        {
            try
            {
                var result = await _httpClient.GetAsync($"/api/buy-course-from-web?message={message}&CourseId={courseId}&PromocodeText={promoCode}&userId={userId}&isWebHook={isWebHook}");
                return result;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> SubscribeLive(string message, long liveId, string promoCode = "", string userId = "", bool isWebHook = false)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/buy-live-from-web?message={message}&liveId={liveId}&PromocodeText={promoCode}&userId={userId}&isWebHook={isWebHook}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }
        public async Task<HttpResponseMessage> InsertInvoiceLog(long invoiceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/insert-invoice-log?invoiceId={invoiceId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> UpdateInvoiceLog(long invoiceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/update-invoice-log?invoiceId={invoiceId}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }


        public async Task<HttpResponseMessage> SubscribeTrack(long trackId, string promoCode = "", string userId = "", bool isWebHook = false)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/TrackSubscription?TrackId={trackId}&PromocodeText={promoCode}&userId={userId}&isWebHook={isWebHook}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetAllStudentTransactions(int page = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GetAllStudentTransactions?Page={page}");
                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }


        public async Task<HttpResponseMessage> FixFailedInvoices()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/fix-failed-invoices");
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
