using Microsoft.AspNetCore.Mvc;
using TolabPortal.DataAccess.Login;

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
        public IActionResult Login()
        {
            return View();
        }
    }
}