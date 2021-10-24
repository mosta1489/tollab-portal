using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;
using Tolab.Common;

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
        public async Task<IActionResult> Home()
        {
            return View();
        }

        [Route("~/Index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("~/Terms")]
        public IActionResult Terms()
        {
            return View("Terms");
        }

        #endregion

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
            if (ModelState.IsValid)
            {
                var phoneNumberWithKey = $"{loginModel.PhoneKey}{loginModel.PhoneNumber}";
                var loginResponse = await _accountService.StudentLogin(phoneNumberWithKey);
                if (loginResponse.IsSuccessStatusCode)
                {
                    var responseString = await loginResponse.Content.ReadAsStringAsync();
                    var studentInfo = JsonConvert.DeserializeObject<Student>(responseString);

                    LoginVerification loginVerification = new LoginVerification();
                    loginVerification.PhoneKey = loginModel.PhoneKey;
                    loginVerification.PhoneNumber = loginModel.PhoneNumber;
                    return View("LoginVerification", loginVerification);
                }
                else
                {
                    // added temporarily to redirect to verification page even phone number is invalid (should be removed later)

                    LoginVerification loginVerification = new LoginVerification();
                    loginVerification.PhoneKey = loginModel.PhoneKey;
                    loginVerification.PhoneNumber = loginModel.PhoneNumber;
                    return View("LoginVerification", loginVerification);

                    // error occured in login page using phone
                    //ViewBag.HasError = true;
                    //return View(loginModel);
                }
            }
            return View(loginModel);
        }

        [Route("~/login/Verification")]
        public IActionResult LoginVerification()
        {
            return View("LoginVerification");
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
                    var studentInfo = JsonConvert.DeserializeObject<Student>(responseString);

                    return View("VerificationSuccess");
                }
                else
                {

                    return View("VerificationSuccess");
                    //ViewBag.HasError = true;
                }
            }
            else
            {
                ViewBag.HasError = true;
            }
            return View();
        }

        #endregion

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
        public IActionResult RegisterInfo(RegisterInfo registerInfo)
        {
            if (ModelState.IsValid)
            {
                RegisterVerification registerVerification = new RegisterVerification();
                registerVerification.PhoneKey = registerInfo.PhoneKey;
                registerVerification.PhoneNumber = registerInfo.PhoneNumber;
                registerVerification.ConditionsAgree = registerInfo.ConditionsAgree;
                registerVerification.Name = registerInfo.Name;
                registerVerification.Email = registerInfo.Email;
                registerVerification.Gender = bool.Parse(registerInfo?.Gender ?? "false");
                registerVerification.Bio = registerInfo.Bio;

                return View("RegisterVerification", registerVerification);
            }
            else
            {
                ViewBag.InvalidInfo = true;
            }
            return View(registerInfo);
        }

        [HttpPost]
        [Route("~/RegisterVerification")]
        public async Task<IActionResult> RegisterVerification(RegisterVerification registerVerification)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student(registerVerification.PhoneKey, registerVerification.PhoneNumber, registerVerification.Name, registerVerification.Email,
                    registerVerification.Gender, registerVerification.Bio, Int32.Parse(registerVerification.VerificationCode));

                var registerVerificationResponse = await _accountService.RegisterStudent(student);
                if (registerVerificationResponse.IsSuccessStatusCode)
                {
                    var studentInfo = CommonUtilities.GetResponseModelFromJson<Student>(registerVerificationResponse);
                    return View("VerificationSuccess");
                }
                else
                {
                    return View("VerificationSuccess");
                    //ViewBag.HasError = true;
                }
            }
            else
            {
                ViewBag.HasError = true;
            }
            return View(registerVerification);
        }

        #endregion
    }
}