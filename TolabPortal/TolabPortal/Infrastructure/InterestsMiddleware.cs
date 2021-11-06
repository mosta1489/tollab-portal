using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Services;

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
            try
            {
                if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
                {
                    var studentProfileResponse = await accountService.GetStudentProfile();
                    var studentProfile =
                        await CommonUtilities.GetResponseModelFromJson<StudentResponse>(studentProfileResponse);

                    if (!studentProfile.Student.Interests.Any() && context.Request.Path.Value != null
                                                                && !context.Request.Path.Value.Contains("/Interest")
                                                                && !context.Request.Path.Value.Contains("/logout"))
                    {
                        context.Response.Redirect("/Interest/RegisterSection");
                        return;
                    }
                }

                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                await context.Request.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var logoutResponse = await accountService.LogoutStudent();
                if (logoutResponse.IsSuccessStatusCode)
                {
                    var logoutResult = await CommonUtilities.GetResponseModelFromJson<StudentLogoutResponse>(logoutResponse);
                }
                context.Response.Redirect("/login");
                return;
            }
        }

        private bool IsPathContainsValue(HttpContext httpContext, string value)
        {
            return httpContext.Request.Path.Value.ToLower().Contains(value.ToLower());
        }
    }
}