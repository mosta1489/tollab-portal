using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;

namespace TolabPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class InterestController : Controller
    {
        private readonly IInterestService _interestService;
        private readonly ISessionManager _sessionManager;

        public InterestController(IInterestService interestService,
            ISessionManager sessionManager)
        {
            _interestService = interestService;
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("RegisterSection")]
        public async Task<IActionResult> RegisterSection(bool hasError)
        {
            ViewBag.HasError = hasError;

            //long sampleCountryId = _sessionManager.CountryId;

            var SectionsResponse = await _interestService.GetSections();

            if (SectionsResponse.IsSuccessStatusCode)
            {
                var responseResult = await CommonUtilities.GetResponseModelFromJson<SectionResponse>(SectionsResponse);
                return View("RegisterSection", responseResult);
            }
            else
            {
            }
            return View("RegisterSection");
        }

        [HttpPost]
        [Route("RegisterSection")]
        public async Task<IActionResult> RegisterSection(string sectionId)
        {
            if (sectionId != null)
            {
                var sectionCategoriesResponse = await _interestService.GetCategoriesBySectionId(long.Parse(sectionId));
                var responseResult = await CommonUtilities.GetResponseModelFromJson<CategoryResponse>(sectionCategoriesResponse);
                //ViewBag.CategoryName = responseResult.
                return View("RegisterCategory", responseResult);
            }
            else
            {
                ViewBag.HasError = true;
                return RedirectToAction("RegisterSection", new { hasError = true });
            }
        }

        [Route("RegisterCategory")]
        public IActionResult RegisterCategory()
        {
            return View("RegisterCategory");
        }

        [HttpPost]
        [Route("RegisterCategory")]
        public async Task<IActionResult> RegisterCategory(string categoryId)
        {
            if (categoryId != null)
            {
                var subCategoriesResponse = await _interestService.GetSubCategoriesByCategoryId(long.Parse(categoryId));
                var responseResult = await CommonUtilities.GetResponseModelFromJson<SubCategoryResponse>(subCategoriesResponse);
                return View("RegisterSubCategory", responseResult);
            }
            else
            {
            }
            return View();
        }

        [Route("RegisterSubCategory")]
        public IActionResult RegisterSubCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("RegisterSubCategory")]
        public async Task<IActionResult> RegisterSubCategory(string categoryId)
        {
            return View();
        }

        [Route("RegisterDepartment")]
        public IActionResult RegisterDepartment()
        {
            return View();
        }

        [HttpPost]
        [Route("RegisterDepartment")]
        public async Task<IActionResult> RegisterDepartment(string subCategoryId)
        {
            return View();
        }

        [Route("GetSubjects")]
        public IActionResult GetSubjects()
        {
            return View();
        }
    }
}