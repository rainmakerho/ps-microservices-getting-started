using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace GloboTicket.Services.EventCatalog
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            try
            {
                Log.Debug("init main function");
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error in init");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
