﻿using Application.Interfaces.IRepositories;
using Infrastructure.EntityFramework.Repositories;

namespace FusionNews_API.WebExtensions
{
    public static class RepositoryCfgExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IChatBotRepository, ChatBotRepository>();

            return services;
        }
    }
}
