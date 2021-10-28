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
        private readonly ISubscribeService _subscribeService;

        public LivesController(ICourseService courseService, ISessionManager sessionManager,
            IAccountService accountService, IInterestService interestService, ISubscribeService subscribeService)
        {
            _courseService = courseService;
            _sessionManager = sessionManager;
            _accountService = accountService;
            _interestService = interestService;
            _subscribeService = subscribeService;
        }

        public async Task<IActionResult> Index()
        {
            var livesResponse = await _courseService.GetLives();
            if (livesResponse.IsSuccessStatusCode)
            {
                var lives = await CommonUtilities.GetResponseModelFromJson<StudentLiveHomeResponse>(livesResponse);
                return View("Index", lives.StudentLives);
            }
            return View("Index", new StudentLiveHome());
        }

        [Route("liveDetails")]
        public async Task<IActionResult> GetLiveDetails(int liveId)
        {
            var livesResponse = await _courseService.GetLiveDetails(liveId);
            if (livesResponse.IsSuccessStatusCode)
            {
                var liveDetails = await CommonUtilities.GetResponseModelFromJson<LiveDetailsResponse>(livesResponse);

                DataAccess.Models.ItemDetails abstractLiveInfo = new DataAccess.Models.ItemDetails();
                abstractLiveInfo.LiveName = liveDetails.LiveDetails.LiveName;
                abstractLiveInfo.PageRootName = "الرئيسية";
                abstractLiveInfo.SectionName = "البث المباشر";
                liveDetails.LiveDetails.ItemDetails = abstractLiveInfo;

                // getting user transactions / subscribtions to check whether it contains the id of current live
                var studentTransactionsResponse = await _subscribeService.GetAllStudentTransactions();
                if (studentTransactionsResponse.IsSuccessStatusCode)
                {
                    var studentTransactions = await CommonUtilities.GetResponseModelFromJson<StudentTransactionsResponse>(studentTransactionsResponse);
                    var IsCurrentStudentSubscribedToLive = studentTransactions.studentTransactionsVM.studentTransactions.Any(t => t.LiveId == liveDetails.LiveDetails.Id);
                    liveDetails.LiveDetails.IsCurrentStudentSubscribed = IsCurrentStudentSubscribedToLive;
                }

                return View("LiveDetails", liveDetails.LiveDetails);
            }
            return View("LiveDetails", new LiveDetails());
        }
    }
}
