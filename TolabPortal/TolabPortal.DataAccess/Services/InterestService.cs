using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tolab.Common;
using System.Net.Http.Json;

namespace TolabPortal.DataAccess.Services
{
    public interface IInterestService
    {
        Task<HttpResponseMessage> GetSections(bool isIncludeSubCategory = false);

        Task<HttpResponseMessage> GetCategoriesBySectionId(long sectionId);

        Task<HttpResponseMessage> GetSubCategoriesByCategoryId(long categoryId);

        Task<HttpResponseMessage> GetDepartmentsBySubCategoryId(long subCategoryId);

        Task<HttpResponseMessage> AddDepartmentToStudent(List<long> departmentIds);
        Task<HttpResponseMessage> DeleteDepartmentByIds(List<long> departmentIds);
        Task<HttpResponseMessage> GetInterestsBeforeEdit();
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

        public async Task<HttpResponseMessage> GetSections( bool isIncludeSubCategory = false)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSections?isIncludeSubCategory={isIncludeSubCategory}");
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

        public async Task<HttpResponseMessage> AddDepartmentToStudent(List<long> departmentIds)
        {
            try
            {
                if (departmentIds == null)
                    throw new ArgumentNullException();

                var sectionsResponse = await _httpClient.PostAsync("/api/AddDepartmentToStudent", new StringContent(JsonConvert.SerializeObject(departmentIds), Encoding.UTF8, "application/json"));
                return sectionsResponse;
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

        public async Task<HttpResponseMessage> GetInterestsBeforeEdit()
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetInterestsBeforeEdit");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> DeleteDepartmentByIds(List<long> departmentIds)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = JsonContent.Create(departmentIds),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"/api/DeleteDepartmentByIds", UriKind.Relative)
                };
                var deleteDepartmentsResponse = await _httpClient.SendAsync(request);
                return deleteDepartmentsResponse;
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