using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolab.Common;
using TolabPortal.DataAccess.Services;
using TolabPortal.DataAccess.Services.Payment;

namespace TolabPortal.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IInterestService, InterestService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMyFatoorahPaymentService, MyFatoorahPaymentService>();
            services.AddHttpClient();
            services.AddScoped<IMyFatoorahClient, MyFatoorahClient>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessionManager, SessionManager>();

            return services;
        }

        public static IServiceCollection ConfigureBundles(this IServiceCollection services)
        {
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/css/commonBundle.css",
                     "css/bootstrap.min.css",
                     "css/bootstrap-rtl.min.css",
                     "css/line-awesome.css",
                     "libcss/owl.carousel.min.css",
                     "css/owl.theme.default.min.css",
                     "css/bootstrap-select.min.css",
                     "css/fancybox.css",
                     "css/tooltipster.bundle.css",
                     "css/intlTelInput.min.css",
                     "css/leaflet.css",
                     "css/style-rtl.css",
                     "css/site.css");

                pipeline.AddJavaScriptBundle("/js/commonBundle.js",
                   "js/jquery-3.4.1.min.js",
                   "js/bootstrap.bundle.min.js",
                   "js/bootstrap-select.min.js",
                   "js/owl.carousel.min.js",
                   "js/isotope.js",
                   "js/waypoint.min.js",
                   "js/jquery.counterup.min.js",
                   "js/intlTelInput-jquery.min.js",
                   "js/fancybox.js",
                   "js/datedropper.min.js",
                   "js/emojionearea.min.js",
                   "js/tooltipster.bundle.min.js",
                   "js/jquery.lazy.min.js",
                   "js/main-rtl.js",
                   "js/site.js");
            });
            return services;
        }
    }
}