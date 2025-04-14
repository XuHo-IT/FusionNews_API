using Application.Interfaces.IRepositories;
using Infrastructure.EntityFramework.Repositories;

namespace FusionNews_API.WebExtensions
{
    public static class RepositoryCfgExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
