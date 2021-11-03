using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Services;
using TolabPortal.Models;

namespace TolabPortal.Infrastructure
{
    public class InterestsMiddleware
    {
        private readonly RequestDelegate _next;

        public InterestsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAccountService accountService)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var studentProfileResponse = await accountService.GetStudentProfile();
                var studentProfile = await CommonUtilities.GetResponseModelFromJson<GetStudentProfileModel>(studentProfileResponse);

                if (!studentProfile.model.Interests.Any() && context.Request.Path.Value != null && !context.Request.Path.Value.Contains("/Interest"))
                {
                    context.Response.Redirect("/Interest/RegisterSection");
                    return;
                }

                if (studentProfile.model.Interests.Any() && context.Request.Path.Value != null && context.Request.Path.Value.Contains("/Interest"))
                {
                    context.Response.Redirect("/Courses/Index");
                    return;
                }
            }
            await _next(context);
        }
    }
}