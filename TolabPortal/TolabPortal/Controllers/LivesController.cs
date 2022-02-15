using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;

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

        public async Task<IActionResult> Index(int page = 1)
        {
            var livesResponse = await _courseService.GetLives(page - 1); // page indices in db are 0-based
            if (livesResponse.IsSuccessStatusCode)
            {
                var lives = await CommonUtilities.GetResponseModelFromJson<StudentLiveHomeResponse>(livesResponse);

                var paginatedLives = new PaginatedStudentLiveHome();
                paginatedLives.StudentLives = lives.StudentLives;
                paginatedLives.PageIndex = page;

                return View("Index", paginatedLives);
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
                    var studentTransactions = await CommonUtilities.GetResponseModelFromJson<StudentTransactionVM>(studentTransactionsResponse);
                    var IsCurrentStudentSubscribedToLive = studentTransactions.studentTransactions.Any(t => t.LiveId == liveDetails.LiveDetails.Id);
                    liveDetails.LiveDetails.IsCurrentStudentSubscribedToLive = IsCurrentStudentSubscribedToLive;
                }

                //// getting teacher image
                //var teacherProfileResponse = await _courseService.GetTeacherById(trackDetails.CoursesByTrackId.Courses.FirstOrDefault().TeacherId);
                //if (teacherProfileResponse.IsSuccessStatusCode)
                //{
                //    var teacherProfile = await CommonUtilities.GetResponseModelFromJson<TeacherResponse>(teacherProfileResponse);
                //    liveDetails.LiveDetails.TeacherPhoto = teacherProfile.Teacher.Photo;
                //}


                if (liveDetails.LiveDetails.MeetingId.HasValue)
                    liveDetails.LiveDetails.MeetingSignature = GenerateSignature(liveDetails.LiveDetails.MeetingId.ToString());
                return View("LiveDetails", liveDetails.LiveDetails);
            }
            return View("LiveDetails", new LiveDetails());
        }

        private string GenerateSignature(string meetingNumber)
        {
            string apiKey = "ZU2qefs0Rp6rLTwOsPq9lQ";
            string apiSecret = "JTpmaAaDaTtlJcjLw7pTDId899RydwLrVQ8R";
            String ts = (ToTimestamp(DateTime.UtcNow.ToUniversalTime()) - 30000).ToString();
            string role = "0";
            string token = GenerateToken(apiKey, apiSecret, meetingNumber, ts, role);
            return token;
        }

        public static long ToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        public static string GenerateToken(string apiKey, string apiSecret, string meetingNumber, string ts, string role)
        {
            string message = String.Format("{0}{1}{2}{3}", apiKey, meetingNumber, ts, role);
            apiSecret = apiSecret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(apiSecret);
            byte[] messageBytesTest = encoding.GetBytes(message);
            string msgHashPreHmac = System.Convert.ToBase64String(messageBytesTest);
            byte[] messageBytes = encoding.GetBytes(msgHashPreHmac);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string msgHash = System.Convert.ToBase64String(hashmessage);
                string token = String.Format("{0}.{1}.{2}.{3}.{4}", apiKey, meetingNumber, ts, role, msgHash);
                var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                return System.Convert.ToBase64String(tokenBytes).TrimEnd('=');
            }
        }
    }
}