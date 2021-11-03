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

                bool IsUserAccessingLoginOrRegisterPage = IsPathContainsValue(context, "login") || IsPathContainsValue(context, "Registerphone")
                    || IsPathContainsValue(context, "RegisterInfo") || IsPathContainsValue(context, "RegisterVerification");

                if (IsUserAccessingLoginOrRegisterPage)
                {
                    if (sessionManager.HasInterests == null || !sessionManager.HasInterests.Value)
                    {
                        if (!context.Request.Path.Value.Contains("/Interest"))
                        {
                            context.Response.Redirect("/Interest/RegisterSection");
                            return;
                        }
                    }
                    else
                    {
                        context.Response.Redirect("/Subjects");
                        return;
                    }
                }

            }
            await _next(context);
        }

        private bool IsPathContainsValue(HttpContext httpContext, string value)
        {
            return httpContext.Request.Path.Value.ToLower().Contains(value.ToLower());
        }
    }
}