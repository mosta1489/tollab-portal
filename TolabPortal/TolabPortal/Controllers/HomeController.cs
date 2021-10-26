using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;

namespace TolabPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;

        public HomeController(IAccountService loginService)
        {
            _accountService = loginService;
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

            var phoneNumberWithKey = $"{loginModel.PhoneKey}{loginModel.PhoneNumber}";
            var loginResponse = await _accountService.StudentLogin(phoneNumberWithKey);
            if (loginResponse.IsSuccessStatusCode)
            {
                var responseString = await loginResponse.Content.ReadAsStringAsync();
                var studentInfo = JsonConvert.DeserializeObject<Student>(responseString);

                var loginVerification = new LoginVerification
                {
                    PhoneKey = loginModel.PhoneKey,
                    PhoneNumber = loginModel.PhoneNumber
                };
                return View("LoginVerification", loginVerification);
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

        [Route("~/login/ReSendVerificationCode")]
        public async Task<IActionResult> ReSendVerificationCode(string phoneKey, string phoneNumber)
        {
            Login login = new Login()
            {
                ConditionsAgree = true,
                PhoneKey = phoneKey,
                PhoneNumber = phoneNumber
            };
            await Login(login);
            var loginVerification = new LoginVerification
            {
                PhoneKey = phoneKey,
                PhoneNumber = phoneNumber
            };
            return View("LoginVerification", loginVerification);
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

        [Route("~/Registerphone")]
        public IActionResult Registerphone()
        {
            return View("RegisterPhone");
        }

        [HttpPost]
        [Route("~/Registerphone")]
        public IActionResult Registerphone(RegisterPhone registerPhone)
        {
            if (ModelState.IsValid)
            {
                RegisterInfo registerInfo = new RegisterInfo();
                registerInfo.PhoneKey = registerPhone.PhoneKey;
                registerInfo.PhoneNumber = registerPhone.PhoneNumber;
                registerInfo.ConditionsAgree = registerPhone.ConditionsAgree;
                return View("RegisterInfo", registerInfo);
            }
            else
            {
                ViewBag.InvalidPhoneNumber = true;
            }
            return View(registerPhone);
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
            if (ModelState.IsValid)
            {
                var registerVerification = new RegisterVerification
                {
                    PhoneKey = registerInfo.PhoneKey,
                    PhoneNumber = registerInfo.PhoneNumber,
                    ConditionsAgree = registerInfo.ConditionsAgree,
                    Name = registerInfo.Name,
                    Email = registerInfo.Email,
                    Gender = bool.Parse(registerInfo?.Gender ?? "false"),
                    Bio = registerInfo.Bio,
                    CountryId = GetCountryIdByCode(registerInfo.PhoneKey)
                };

                var student = new Student(registerVerification.PhoneKey, registerVerification.PhoneNumber, registerVerification.Name, registerVerification.Email,
                    registerVerification.Gender, registerVerification.Bio, registerVerification.CountryId);

                var registerVerificationResponse = await _accountService.RegisterStudent(student);
                if (registerVerificationResponse.IsSuccessStatusCode)
                {
                    var studentInfo = CommonUtilities.GetResponseModelFromJson<LoginVerificationSuccessResponseModel>(registerVerificationResponse);
                    return View("RegisterVerification", registerVerification);
                }
                else
                {
                    var responseString = await registerVerificationResponse.Content.ReadAsStringAsync();
                    var errorModel = JsonConvert.DeserializeObject<ApiErrorModel>(responseString);
                    ViewBag.ErrorMessage = errorModel.errors.message;

                    return View(registerInfo);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "من فضلك ادخل البيانات المطلوبه";
                return View(registerInfo);
            }
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
                else
                {
                    ViewBag.ErrorMessage = "كود التفعيل غير صحيح";
                    return View("RegisterVerification", registerVerification);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "حدث خطا اثناء العمليه ";
            }
            return View();
        }

        private async Task LoginUser(LoginVerificationSuccessResponseModel user)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsIdentity identity = new ClaimsIdentity(await GetUserClaims(user), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = true });
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = true });
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(LoginVerificationSuccessResponseModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.model.Id.ToString()),
                new Claim("AccessToken", user.model.Token.First().access_token),
                new Claim("CountryId", user.model.CountryId.ToString()),
                new Claim("CountryCode", user.model.CountryCode),
                new Claim("HasInterests", user.model.Interests.Any().ToString())
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
    }
}