using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<IActionResult> RegisterSection(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            var sectionsResponse = await _interestService.GetSections();

            if (sectionsResponse.IsSuccessStatusCode)
            {
                var responseResult = await CommonUtilities.GetResponseModelFromJson<SectionResponse>(sectionsResponse);
                return View("RegisterSection", responseResult);
            }

            return View("RegisterSection");
        }

        [HttpPost]
        [Route("RegisterSection")]
        public async Task<IActionResult> RegisterSection(SectionResponse sectionResponse, string sectionId)
        {
            if (sectionId != null)
            {
                var sectionCategoriesResponse = await _interestService.GetCategoriesBySectionId(long.Parse(sectionId));
                if (sectionCategoriesResponse.IsSuccessStatusCode)
                {
                    var selectedSection = sectionResponse.Sections.FirstOrDefault(s => s.Id == long.Parse(sectionId));
                    var responseResult = await CommonUtilities.GetResponseModelFromJson<CategoryResponse>(sectionCategoriesResponse);
                    responseResult.SelectedSection = selectedSection;
                    return View("RegisterCategory", responseResult);
                }
                else
                {
                    var responseResult = await CommonUtilities.GetResponseModelFromJson<CategoryResponse>(sectionCategoriesResponse);
                    var errorMessage = responseResult.Errors.Message;
                    return RedirectToAction("RegisterSection", new { errorMessage = errorMessage });
                }
            }
            else
            {
                var errorMessage = "لم يتم اختيار مرحلة";
                return RedirectToAction("RegisterSection", new { errorMessage = errorMessage });
            }
        }

        [Route("RegisterCategory")]
        public IActionResult RegisterCategory(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View("RegisterCategory");
        }

        [HttpPost]
        [Route("RegisterCategory")]
        public async Task<IActionResult> RegisterCategory(CategoryResponse categoryResponse, string categoryId)
        {
            if (categoryId != null)
            {
                var subCategoriesResponse = await _interestService.GetSubCategoriesByCategoryId(long.Parse(categoryId));
                if (subCategoriesResponse.IsSuccessStatusCode)
                {
                    var selectedCategory = categoryResponse.Categories.FirstOrDefault(s => s.Id == long.Parse(categoryId));
                    var responseResult = await CommonUtilities.GetResponseModelFromJson<SubCategoryResponse>(subCategoriesResponse);

                    // getting departments for each sub category
                    for (int i = 0; i < responseResult.SubCategories.Count; ++i)
                    {
                        var subCategory = responseResult.SubCategories[i];
                        var departmenResponse = await _interestService.GetDepartmentsBySubCategoryId(subCategory.Id);
                        var departments = await CommonUtilities.GetResponseModelFromJson<DepartmentResponse>(departmenResponse);
                        responseResult.SubCategories[i].Departments = departments.Departments;

                        responseResult.SubCategories[i].Name = responseResult.SubCategories[i].Name.Replace("\t", "");
                        responseResult.SubCategories[i].NameLT = responseResult.SubCategories[i].NameLT.Replace("\t", "");
                    }
                    responseResult.SelectedCategory = selectedCategory;
                    return View("RegisterSubCategory", responseResult);
                }
                else
                {
                    var responseResult = await CommonUtilities.GetResponseModelFromJson<CategoryResponse>(subCategoriesResponse);
                    var errorMessage = responseResult.Errors.Message;
                    return RedirectToAction("RegisterCategory", new { errorMessage = errorMessage });
                }
            }
            else
            {
                var errorMessage = "لم يتم اختيار مرحلة";
                return RedirectToAction("RegisterCategory", new { errorMessage = errorMessage });
            }
        }

        [Route("RegisterSubCategory")]
        public IActionResult RegisterSubCategory(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View("RegisterSubCategory");
        }

        [HttpPost]
        [Route("RegisterSubCategory")]
        public async Task<IActionResult> RegisterSubCategory(SubCategoryResponse subCategoryResponse, string subCategoryId)
        {
            if (subCategoryId != null)
            {
                var subCategoriesResponse = await _interestService.GetDepartmentsBySubCategoryId(long.Parse(subCategoryId));
                if (subCategoriesResponse.IsSuccessStatusCode)
                {
                    var selectedSubCategory = subCategoryResponse.SubCategories.FirstOrDefault(s => s.Id == long.Parse(subCategoryId));
                    var responseResult = await CommonUtilities.GetResponseModelFromJson<DepartmentResponse>(subCategoriesResponse);
                    responseResult.SelectedSubCategory = selectedSubCategory;
                    return View("RegisterDepartment", responseResult);
                }
                else
                {
                    var responseResult = await CommonUtilities.GetResponseModelFromJson<DepartmentResponse>(subCategoriesResponse);
                    var errorMessage = responseResult.Errors.Message;
                    return RedirectToAction("RegisterSubCategory", new { errorMessage = errorMessage });
                }
            }
            else
            {
                var errorMessage = "لم يتم اختيار مرحلة";
                return RedirectToAction("RegisterSubCategory", new { errorMessage = errorMessage });
            }
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