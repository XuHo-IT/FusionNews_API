using Application.Interfaces.Services;
using Application.Interfaces.IServices;
using FusionNews_API.Services.News;
using Infrastructure.LogProvider;
using Infrastructure.Services;

namespace FusionNews_API.WebExtensions
{
    public static class ServiceCfgExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddSingleton<Log>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IPostService, PostService>();


            return services;
        }
    }
}
