using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Login;
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

        public async Task<IActionResult> Index()
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
        [Route("~/LoginPhone")]
        public async Task<IActionResult> LoginPhone(LoginPhone loginModel)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _loginService.StudentLogin(loginModel.PhoneNumber);
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