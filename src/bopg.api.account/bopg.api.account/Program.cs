using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace bopg.api.account
{
    public class Program
    {
        #region -= Properties =-
        public static IConfiguration Configuration { get; set; }
        #endregion

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            SetEnv();

            CreateWebHostBuilder(args).Build().Run();
        }

        private static void SetEnv()
        {
            Configuration.GetSection("GrayLog:ServerURL").Value =
                Environment.GetEnvironmentVariable("GRAYLOG_SERVER") ?? Configuration.GetSection("GrayLog:ServerURL").Value;
            Configuration.GetSection("GrayLog:Port").Value =
                Environment.GetEnvironmentVariable("GRAYLOG_PORT") ?? Configuration.GetSection("GrayLog:Port").Value;

            Configuration.GetSection("API:Admin").Value =
                Environment.GetEnvironmentVariable("API_ADMIN_URL") ?? Configuration.GetSection("API:Admin").Value;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(Configuration.GetSection("HostingURL").Value);
    }
}
