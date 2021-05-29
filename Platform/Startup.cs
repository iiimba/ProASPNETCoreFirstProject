using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.Models;
using Platform.Services;
using System;
using System.Threading.Tasks;

namespace Platform
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configService)
        {
            Configuration = configService;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedSqlServerCache(opts =>
            {
                opts.ConnectionString = Configuration["ConnectionStrings:CacheConnection"];
                opts.SchemaName = "dbo";
                opts.TableName = "DataCache";
            });
            services.AddResponseCaching();
            services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

            services.AddDbContext<CalculationContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:CalcConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });

            services.AddTransient<SeedData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, SeedData seedData)
        {
            app.UseDeveloperExceptionPage();
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");

                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            bool cmdLineInit = (Configuration["INITDB"] ?? "false") == "true";
            if (env.IsDevelopment() || cmdLineInit)
            {
                seedData.SeedDatabase();
                if (cmdLineInit)
                {
                    lifetime.StopApplication();
                }
            }
        }
    }
}
