using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using FusionNews_API.Services.Comments;
using FusionNews_API.Services.News;
using FusionNews_API.Services.Posts;
using FusionNews_API.Services.Quetions;
using Infrastructure.LogProvider;
using Infrastructure.Services;


namespace FusionNews_API.WebExtensions
{
    public static class ServiceCfgExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddSingleton<Log>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IChatBotService, ChatBotService>();

            return services;
        }
    }
}
