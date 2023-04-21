using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace TimeChimp.Backend.Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            // Set properties and call methods on options
                        })
                        .UseSerilog((context, config) => 
                        {
                            config.ReadFrom.Configuration(context.Configuration)
                                  .WriteTo.Console(new JsonFormatter(renderMessage: true))
                                  .Enrich.FromLogContext();
                        })
                        .UseStartup<Startup>();
                });
    }
}