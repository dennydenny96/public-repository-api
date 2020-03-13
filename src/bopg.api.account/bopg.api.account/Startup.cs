using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace bopg.api.account
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // Enable GZIP Compression
            services.Configure<GzipCompressionProviderOptions>(opt => opt.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            // Configure HttpClient
            services.AddHttpClient("BAS", client =>
            {
                client.BaseAddress = new Uri(Program.Configuration.GetSection("API:BAS").Value);
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(
                context => context.Request.Path.ToString().EndsWith(".api"),
                appBranch =>
                {
                    appBranch.UseAPIHandler();
                });

            app.Map("/doc", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    context.Response.Headers.Add("Content-Type", "text/plain");
                    await context.Response.WriteAsync(Doc.DocAllAPI.Write());
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Backoffice Payment Gateway Account V1.0");
            });
        }
    }
}
