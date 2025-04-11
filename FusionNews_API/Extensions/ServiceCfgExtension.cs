using FusionNews_API.Interfaces.News;
using FusionNews_API.Services.News;

namespace FusionNews_API.WebExtensions
{
    public static class ServiceCfgExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();

            return services;
        }
    }
}
