using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Tolab.Common;

namespace TolabPortal.Infrastructure
{
    public class InterestsMiddleware
    {
        private readonly RequestDelegate _next;

        public InterestsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISessionManager sessionManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (sessionManager.HasInterests == null || !sessionManager.HasInterests.Value)
                {
                    if (!context.Request.Path.Value.Contains("/Interest"))
                    {
                        context.Response.Redirect("/Interest/RegisterSection");
                        return;
                    }
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