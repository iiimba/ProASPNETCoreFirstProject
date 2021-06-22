using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Filters;
using WebApp.Models;
using WebApp.TagHelpers;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:ProductConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddSingleton<CitiesData>();
            //services.AddTransient<ITagHelperComponent, TimeTagHelperComponent>();
            //services.AddTransient<ITagHelperComponent, TableFooterTagHelperComponent>();

            services.Configure<AntiforgeryOptions>(opts =>
            {
                opts.HeaderName = "X-XSRF-TOKEN";
            });

            services.Configure<MvcOptions>(opts => opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Please enter a value"));

            services.AddScoped<GuidResponseAttribute>();
            //services.Configure<MvcOptions>(opts =>
            //{
            //    opts.Filters.Add<HttpsOnlyAttribute>();
            //    opts.Filters.Add(new MessageAttribute("This is the globally-scoped filter"));
            //});
        }

        public void Configure(IApplicationBuilder app, DataContext context, IAntiforgery antiforgery)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                if (!context.Request.Path.StartsWithSegments("/api"))
                {
                    context.Response.Cookies.Append("XSRF-TOKEN", antiforgery.GetAndStoreTokens(context).RequestToken, new CookieOptions { HttpOnly = false });
                }

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("forms", "controllers/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
            SeedData.SeedDatabase(context);
        }
    }
}
