using Application.Interfaces;
using FusionNews_API.Services.News;
using Microsoft.Extensions.DependencyInjection;

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
