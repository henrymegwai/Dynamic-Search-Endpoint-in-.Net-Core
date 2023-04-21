using Microsoft.Extensions.DependencyInjection;
using TimeChimp.Core.Configs;
using TimeChimp.Core.Interfaces.IManagers;
using TimeChimp.Core.Interfaces.IServices;
using TimeChimp.Core.Managers;
using TimeChimp.Infrastructure.Services;

namespace TimeChimp.Backend.Assessment
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extensibility point for adding general services available to all components
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        /// <summary>
        /// Extensibility point for adding services constructed using a specialised provider e.g.: country specific services
        /// </summary>
        /// <param name="services"></param>
        public static void AddServiceProviders(this IServiceCollection services)
        {
            services.AddScoped<IRssFeedManager, RssFeedManager>();
            services.AddScoped<IRssFeedService, RssFeedService>();
        }
    }
}
