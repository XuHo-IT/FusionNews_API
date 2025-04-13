using Microsoft.Extensions.DependencyInjection;

namespace FusionNews_API.WebExtensions
{
    public static class RepositoryCfgExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            return services;
        }
    }
}
