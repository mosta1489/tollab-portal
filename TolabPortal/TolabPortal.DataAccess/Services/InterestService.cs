using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tolab.Common;
using System.Net;

namespace TolabPortal.DataAccess.Services
{
    public interface IInterestService
    {
        Task<HttpResponseMessage> GetSectionsByCountryId(long countryId);
        Task<HttpResponseMessage> GetCategoriesBySectionId(long SectionId);
        Task<HttpResponseMessage> GetSubCategoriesByCategoryId(long CategoryId);
        Task<HttpResponseMessage> GetDepartmentsBySubCategoryId(long SubCategoryId);
        Task<HttpResponseMessage> GetCoursesByDepartmentId(long DepartmentId);
        Task<HttpResponseMessage> GetSubjectsWithTracksByDepartmentId(long DepartmentId);
    }

    public class InterestService : IInterestService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public InterestService(IOptions<ApplicationConfig> options)
        {
            var config = options.Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(config.ApiUrl) };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        public async Task<HttpResponseMessage> GetSectionsByCountryId(long countryId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSectionsByCountryIdTemp?countryId={countryId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetCategoriesBySectionId(long SectionId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetCategoriesBySectionIdTemp?SectionId={SectionId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetSubCategoriesByCategoryId(long CategoryId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSubCategoriesByCategoryIdTemp?CategoryId={CategoryId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetDepartmentsBySubCategoryId(long SubCategoryId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetDepartmentsBySubCategoryIdTemp?SubCategoryId={SubCategoryId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetCoursesByDepartmentId(long DepartmentId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetCoursesByDepartmentIdTemp?DepartmentId={DepartmentId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        public async Task<HttpResponseMessage> GetSubjectsWithTracksByDepartmentId(long DepartmentId)
        {
            try
            {
                var sectionsResponse = await _httpClient.GetAsync($"/api/GetSubjectsWithTracksByDepartmentIdTemp?DepartmentId={DepartmentId}");
                return sectionsResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.ReasonPhrase = ex.Message;
                return errorResponse;
            }
        }

        //public async string GetSectionNameBySectionId(long sectionId)
        //{

        //}
    }
}
