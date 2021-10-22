using Microsoft.AspNetCore.Mvc;
using TolabPortal.DataAccess.Login;
using TolabPortal.DataAccess.Login.Models;
using TolabPortal.DataAccess;
using TolabPortal.Models;

namespace TolabPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            //Landing Page waiting for design
            return View();
        }

        [Route("~/login")]
        public IActionResult LoginPhone()
        {
            return View("LoginPhone");
        }

        [HttpPost]
        public IActionResult LoginPhone(LoginPhone loginModel)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = _loginService.StudentLogin(loginModel.PhoneNumber);

            }
            return View(loginModel);
        }

        [Route("~/login/ActivationCode")]
        public IActionResult LoginCode()
        {
            return View("LoginCode");
        }

        [HttpPost]
        public IActionResult LoginCode(LoginCode loginCode)
        {
            return View();
        }



    }
}