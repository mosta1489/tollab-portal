using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TolabPortal.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}