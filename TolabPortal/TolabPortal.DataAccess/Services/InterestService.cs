using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tolab.Common;

namespace TolabPortal.DataAccess.Services
{
    public interface IInterestService
    {
        Task<HttpResponseMessage> GetSections();

        Task<HttpResponseMessage> GetCategoriesBySectionId(long sectionId);

        Task<HttpResponseMessage> GetSubCategoriesByCategoryId(long categoryId);

        Task<HttpResponseMessage> GetDepartmentsBySubCategoryId(long ubCategoryId);

        Task<HttpResponseMessage> GetCoursesByDepartmentId(long departmentId);

        Task<HttpResponseMessage> GetSubjectsWithTracksByDepartmentId(long departmentId);
    }

    public class InterestService : IInterestService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionManager _sessionManager;

        public InterestService(IOptions<ApplicationConfig> options,
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

        public async Task<HttpResponseMessage> GetSections()
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSections");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetCategoriesBySectionId(long sectionId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetCategoriesWithSubCategoriesBySectionId?sectionId={sectionId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetSubCategoriesByCategoryId(long categoryId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSubCategoriesByCategoryId?categoryId={categoryId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetDepartmentsBySubCategoryId(long subCategoryId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetDepartmentsBySubCategoryId?subCategoryId={subCategoryId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetCoursesByDepartmentId(long departmentId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetCoursesByDepartmentId?departmentId={departmentId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetSubjectsWithTracksByDepartmentId(long departmentId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSubjectsWithTracksByDepartmentId?departmentId={departmentId}");
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