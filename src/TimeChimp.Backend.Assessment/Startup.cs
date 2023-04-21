using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TimeChimp.Core.Configs;
using Swashbuckle.AspNetCore.SwaggerGen;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace TimeChimp.Backend.Assessment
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RssFeedConfig>(Configuration.GetSection("RssFeedConfig"));
            services.AddApi(Configuration);
            services.AddServices();
            services.AddServiceProviders();
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseApi(Configuration, Environment);

            logger.LogInformation(default(EventId), $"{Assembly.GetExecutingAssembly().GetName().Name} started");
        }
    }
}
