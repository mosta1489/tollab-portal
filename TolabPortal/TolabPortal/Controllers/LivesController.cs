using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;

namespace TolabPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class LivesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISessionManager _sessionManager;
        private readonly IAccountService _accountService;
        private readonly IInterestService _interestService;

        public LivesController(ICourseService courseService, ISessionManager sessionManager, IAccountService accountService, IInterestService interestService)
        {
            _courseService = courseService;
            _sessionManager = sessionManager;
            _accountService = accountService;
            _interestService = interestService;
        }

        public async Task<IActionResult> Index()
        {
            var livesResponse = await _courseService.GetTopLives();
            if (livesResponse.IsSuccessStatusCode)
            {
                var lives = await CommonUtilities.GetResponseModelFromJson<StudentLiveHomeResponse>(livesResponse);
                return View("Index", lives.StudentLives);
            }
            return View("Index", new StudentLiveHome());
        }


    }
}
