using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;
using TolabPortal.ViewModels;

namespace TolabPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IInterestService _interestService;
        private readonly IMapper _mapper;

        public HomeController(IAccountService loginService, IInterestService interestService, IMapper mapper)
        {
            _accountService = loginService;
            _interestService = interestService;
            _mapper = mapper;
        }

        #region Home and Terms

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Subjects");
            //return RedirectToAction("RegisterSection", "Interest");
            return View();
        }

        [Route("~/Terms")]
        public IActionResult Terms()
        {
            return View("Terms");
        }

        #endregion Home and Terms

        #region Login

        [Route("~/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("~/Login")]
        public async Task<IActionResult> Login(Login loginModel)
        {
            if (!ModelState.IsValid) return View(loginModel);

            var loginResponse = await _accountService.StudentCredentialsLogin(loginModel.UserName, loginModel.Password, loginModel.RememberMe);
            if (loginResponse.IsSuccessStatusCode)
            {
                var responseString = await loginResponse.Content.ReadAsStringAsync();
                var studentInfo = JsonConvert.DeserializeObject<LoginVerificationSuccessResponseModel>(responseString);

                await LoginUser(studentInfo);
                return RedirectToAction("LoginVerificationSuccess");
            }
            else
            {
                var responseString = await loginResponse.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ApiErrorModel>(responseString);
                ViewBag.ErrorMessage = errorModel.errors.message;
                return View(loginModel);
            }
        }

        [Route("~/login/Verification")]
        public IActionResult LoginVerification()
        {
            return View("LoginVerification");
        }

        [Route("~/register/ReSendVerificationCode")]
        public async Task<IActionResult> ReSendVerificationCode(string phoneKey, string phoneNumber)
        {
            TempData.TryGetValue("RegisterModel", out var registerModelJson);
            if (registerModelJson == null)
            {
                ViewBag.InvalidDataError = "حدث خطأ ما اعد المحاولة";
                return RedirectToAction("Register");
            }
            
            var registerModel = JsonConvert.DeserializeObject<RegisterModel>(registerModelJson.ToString());
            await _accountService.StudentLogin(phoneKey + phoneNumber);
            return View("RegisterVerification", new RegisterVerification(registerModel));
        }

        [HttpPost]
        [Route("~/login/Verification")]
        public async Task<IActionResult> LoginVerification(LoginVerification loginVerification)
        {
            if (ModelState.IsValid)
            {
                var loginVerificationResponse = await _accountService.VerifyStudentLogin(loginVerification.PhoneKey, loginVerification.PhoneNumber, loginVerification.ActivationCode);

                if (loginVerificationResponse.IsSuccessStatusCode)
                {
                    var responseString = await loginVerificationResponse.Content.ReadAsStringAsync();
                    var studentInfo = JsonConvert.DeserializeObject<LoginVerificationSuccessResponseModel>(responseString);

                    await LoginUser(studentInfo);
                    var claims = await GetUserClaims(studentInfo);

                    if (studentInfo.model.Interests.Count > 0)
                    {
                        return RedirectToAction("Index", "Subjects");
                    }
                    return RedirectToAction("LoginVerificationSuccess");
                }
                else
                {
                    ViewBag.ErrorMessage = "كود التفعيل غير صحيح";
                    return View("LoginVerification", loginVerification);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "حدث خطا اثناء العمليه ";
            }
            return View();
        }

        [Authorize]
        [Route("~/login/Verification/Success")]
        public IActionResult LoginVerificationSuccess()
        {
            return View("LoginVerificationSuccess"); 
        }

        #endregion Login

        #region Register

        [Route("~/Register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [Route("~/Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (registerModel.Password != registerModel.RePassword)
            {
                ViewBag.InvalidDataError = "كلمتى المرور غير متطابقتين";
                return View(registerModel);
            }

            if (!registerModel.UserPoliciesAgreed)
            {
                ViewBag.InvalidDataError = "برجاء الموافقة على الشروط والأحكام";
                return View(registerModel);
            }


            var student = new Student(registerModel.PhoneKey, registerModel.PhoneNumber, registerModel.UserName, registerModel.Email,
                bool.Parse(registerModel.Gender ?? "false"), registerModel.Bio, GetCountryIdByCode(registerModel.PhoneKey), registerModel.Password);

            var registerVerificationResponse = await _accountService.RegisterStudent(student);
            if (registerVerificationResponse.IsSuccessStatusCode)
            {
                var studentInfo = CommonUtilities.GetResponseModelFromJson<LoginVerificationSuccessResponseModel>(registerVerificationResponse);

                TempData["RegisterModel"] = JsonConvert.SerializeObject(registerModel);
                return View("RegisterVerification", new RegisterVerification(registerModel.UserName, registerModel.Email, registerModel.PhoneNumber, registerModel.Password, registerModel.UserPoliciesAgreed, registerModel.PhoneKey, registerModel.Gender, registerModel.Bio));
            }

            var responseString = await registerVerificationResponse.Content.ReadAsStringAsync();
            var errorModel = JsonConvert.DeserializeObject<ApiErrorModel>(responseString);
            ViewBag.InvalidDataError = errorModel?.errors?.message;
            return View(registerModel);
        }

        [Route("~/RegisterInfo")]
        public IActionResult RegisterInfo()
        {
            return View("RegisterInfo");
        }

        [HttpPost]
        [Route("~/RegisterInfo")]
        public async Task<IActionResult> RegisterInfo(RegisterInfo registerInfo)
        {
            return Ok();
            //if (ModelState.IsValid)
            //{
            //    var registerVerification = new RegisterVerification
            //    {
            //        PhoneKey = registerInfo.PhoneKey,
            //        PhoneNumber = registerInfo.PhoneNumber,
            //        ConditionsAgree = registerInfo.ConditionsAgree,
            //        Name = registerInfo.Name,
            //        Email = registerInfo.Email,
            //        Gender = bool.Parse(registerInfo?.Gender ?? "false"),
            //        Bio = registerInfo.Bio,
            //        CountryId = GetCountryIdByCode(registerInfo.PhoneKey)
            //    };

            //    var student = new Student(registerVerification.PhoneKey, registerVerification.PhoneNumber, registerVerification.Name, registerVerification.Email,
            //        registerVerification.Gender, registerVerification.Bio, registerVerification.CountryId);

            //    var registerVerificationResponse = await _accountService.RegisterStudent(student);
            //    if (registerVerificationResponse.IsSuccessStatusCode)
            //    {
            //        var studentInfo = CommonUtilities.GetResponseModelFromJson<LoginVerificationSuccessResponseModel>(registerVerificationResponse);
            //        return View("RegisterVerification", registerVerification);
            //    }
            //    else
            //    {
            //        var responseString = await registerVerificationResponse.Content.ReadAsStringAsync();
            //        var errorModel = JsonConvert.DeserializeObject<ApiErrorModel>(responseString);
            //        ViewBag.ErrorMessage = errorModel.errors.message;

            //        return View(registerInfo);
            //    }
            //}
            //else
            //{
            //    ViewBag.ErrorMessage = "من فضلك ادخل البيانات المطلوبه";
            //    return View(registerInfo);
            //}
        }

        [HttpPost]
        [Route("~/RegisterVerification")]
        public async Task<IActionResult> RegisterVerification(RegisterVerification registerVerification)
        {
            if (ModelState.IsValid)
            {
                var loginVerificationResponse = await _accountService.VerifyStudentLogin(registerVerification.PhoneKey, registerVerification.PhoneNumber, registerVerification.VerificationCode);
                if (loginVerificationResponse.IsSuccessStatusCode)
                {
                    var responseString = await loginVerificationResponse.Content.ReadAsStringAsync();
                    var studentInfo = JsonConvert.DeserializeObject<LoginVerificationSuccessResponseModel>(responseString);

                    await LoginUser(studentInfo);
                    return RedirectToAction("LoginVerificationSuccess");
                }

                ViewBag.ErrorMessage = "كود التفعيل غير صحيح";
                return View("RegisterVerification", registerVerification);
            }

            ViewBag.ErrorMessage = "حدث خطا اثناء العمليه ";
            return View();
        }

        private async Task LoginUser(LoginVerificationSuccessResponseModel user)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var identity = new ClaimsIdentity(GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = true });
        }

        private IEnumerable<Claim> GetUserClaims(LoginVerificationSuccessResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.model.Id.ToString()),
                new Claim("AccessToken", user.model.Token.First().access_token),
                new Claim("CountryId", user.model.CountryId.ToString()),
                new Claim("CountryCode", user.model.CountryCode),
                new Claim("UserName", user.model.Name),
                new Claim("UserPhoto", user.model.Photo?.ToString() ?? string.Empty),
            };

            return claims;
        }

        public int GetCountryIdByCode(string code)
        {
            //kuwait
            if (code == "+965")
                return 3;
            //Egypt
            else if (code == "+20")
                return 20011;
            //jordan
            else if (code == "+962")
                return 20012;

            return 0;
        }

        #endregion Register

        #region Logout
        [Route("~/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var logoutResponse = await _accountService.LogoutStudent();
            if (logoutResponse.IsSuccessStatusCode)
            {
                var logoutResult = await CommonUtilities.GetResponseModelFromJson<StudentLogoutResponse>(logoutResponse);
            }
            return View("Index");
        }
        #endregion

        #region Edit Profile

        [Route("~/Profile/Edit")]
        public async Task<IActionResult> EditProfileAsync()
        {
            var studentProfileResponse = await _accountService.GetStudentProfile();
            if (studentProfileResponse.IsSuccessStatusCode)
            {
                var studentProfile = await CommonUtilities.GetResponseModelFromJson<StudentResponse>(studentProfileResponse);

                StudentProfileViewModel studentProfileViewModel = _mapper.Map<StudentProfileViewModel>(studentProfile.Student);

                var interestsResponse = await _interestService.GetInterestsBeforeEdit();
                if (interestsResponse.IsSuccessStatusCode)
                {
                    var studentInterests = await CommonUtilities.GetResponseModelFromJson<CategoryResponse>(interestsResponse);
                    studentProfileViewModel.Categories = studentInterests.Categories;
                }

                return View("EditProfile", studentProfileViewModel);
            }
            return View("EditProfile");
        }

        [HttpPost]
        [Route("~/Profile/Edit")]
        public async Task<IActionResult> EditProfileAsync(StudentProfileViewModel studentProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                var studentProfileResponse = await _accountService.GetStudentProfile();
                if (studentProfileResponse.IsSuccessStatusCode)
                {
                    var studentProfile = await CommonUtilities.GetResponseModelFromJson<StudentResponse>(studentProfileResponse);
                    var currentStudentProfileViewModel = _mapper.Map<StudentProfileViewModel>(studentProfile.Student);

                    Student updatedStudent = studentProfile.Student;
                    updatedStudent.Name = studentProfileViewModel.Name;
                    updatedStudent.Gender = studentProfileViewModel.Gender;
                    updatedStudent.Bio = studentProfileViewModel.Bio;

                    var updatedStudentProfileResponse = await _accountService.UpdateStudentProfile(updatedStudent);
                    if (updatedStudentProfileResponse.IsSuccessStatusCode)
                    {
                        var updatedStudentProfile = await CommonUtilities.GetResponseModelFromJson<StudentResponse>(updatedStudentProfileResponse);
                        var updatedStudentProfileViewModel = _mapper.Map<StudentProfileViewModel>(updatedStudentProfile.Student);
                        return View("EditProfile", updatedStudentProfileViewModel);
                    }
                    return View("EditProfile", currentStudentProfileViewModel);
                }
                else // couldn't get user profile casuse of some server error
                {
                    return View("Error500");
                }
            }
            return RedirectToAction("EditProfile");
        }

        #endregion

        #region Error pages
        [Route("~/NotFound")]
        public async Task<IActionResult> NotFound()
        {
            return View("Error404");
        }

        [Route("~/ServerError")]
        public async Task<IActionResult> ServerError()
        {
            return View("Error500");
        }
        #endregion
    }
}