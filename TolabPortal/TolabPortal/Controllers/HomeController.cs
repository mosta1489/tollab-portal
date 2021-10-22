using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TolabPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("~/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}