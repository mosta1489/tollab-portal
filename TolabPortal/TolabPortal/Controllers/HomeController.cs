using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index()
        {
            //Landing Page waiting for design
            return View();
        }

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
                    ViewBag.PhoneKey = studentInfo.PhoneKey;
                    ViewBag.PhoneNumber = studentInfo.Phone;
                    return View("LoginVerification");
                }
                else
                {
                    // added temporarily to redirect to verification page even phone number is invalid (should be removed later)
                    ViewBag.PhoneKey = loginModel.PhoneKey;
                    ViewBag.PhoneNumber = loginModel.PhoneNumber;
                    return View("LoginVerification");

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
            return View();
        }

        #endregion


    }
}