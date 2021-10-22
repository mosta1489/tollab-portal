using Microsoft.Extensions.DependencyInjection;
using TolabPortal.DataAccess.Login;

namespace TolabPortal.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();

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
                     "css/fancybox.css",
                     "css/tooltipster.bundle.css",
                     "css/leaflet.css",
                     "css/style-rtl.css",
                     "css/site.css");

                pipeline.AddJavaScriptBundle("/js/commonBundle.js",
                   "js/jquery-3.4.1.min.js",
                   "js/bootstrap.bundle.min.js",
                   "js/bootstrap-select.min.js",
                   "js/owl.carousel.min.js",
                   "js/waypoint.min.js",
                   "js/jquery.counterup.min.js",
                   "js/main-rtl.js",
                   "js/site.js");
            });
            return services;
        }
    }
}