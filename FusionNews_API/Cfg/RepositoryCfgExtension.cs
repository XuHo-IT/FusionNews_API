using Application.Interfaces;
using Infrastructure.EntityFramework.Repositories;

namespace FusionNews_API.WebExtensions
{
    public static class RepositoryCfgExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            services.AddScoped<INewsRepository, NewsRepository>();
            return services;
        }
    }
}
