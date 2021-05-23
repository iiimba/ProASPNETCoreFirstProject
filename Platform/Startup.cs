using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        private IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MessageOptions>(options =>
            {
                options.CityName = "Albany";
            });

            services.Configure<RouteOptions>(opts =>
            {
                opts.ConstraintMap.Add("countryName", typeof(CountryRouteConstraint));
            });

            ///services.AddScoped(serviceProvider => {
            ///    var typeName = Configuration["services:IResponseFormatter"];
            ///    return (IResponseFormatter)ActivatorUtilities.CreateInstance(
            ///        serviceProvider, 
            ///        typeName == null
            ///            ? typeof(GuidService) 
            ///            : Type.GetType(typeName, true));
            ///            
            ///});
            ///
            services.AddScoped<IResponseFormatter, TextResponseFormatter>();
            services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
            services.AddScoped<IResponseFormatter, GuidService>();

            services.AddSingleton(typeof(ICollection<>), typeof(List<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseMiddleware<WeatherMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/middleware/function")
            //    {
            //        var formatter = context.RequestServices.GetService<IResponseFormatter>();

            //        //var service = app.ApplicationServices.GetRequiredService<IResponseFormatter>();

            //        await formatter.Format(context, "Middleware Function: It is snowing in Chicago");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            app.UseEndpoints(endpoints => {
                //endpoints.MapEndpoint<WeatherEndpoint>("/endpoint/class");
                //endpoints.MapGet("/endpoint/function", async context =>
                //{
                //    var formatter = context.RequestServices.GetService<IResponseFormatter>();

                //    await formatter.Format(context, "Endpoint Function: It is sunny in LA");
                //});

                //endpoints.MapGet("/single", async context => {
                //    var formatter = context.RequestServices.GetService<IResponseFormatter>();
                //    await formatter.Format(context, "Single service");
                //});

                //endpoints.MapGet("/", async context => {
                //    var formatter = context.RequestServices.GetServices<IResponseFormatter>().First(f => f.RichOutput);
                //    await formatter.Format(context, "Multiple services");
                //});

                endpoints.MapGet("/string", async context => {
                    var collection = context.RequestServices.GetService<ICollection<string>>();
                    collection.Add($"Request: {DateTime.Now.ToLongTimeString()}");
                    foreach (var str in collection)
                    {
                        await context.Response.WriteAsync($"String: {str}\n");
                    }

                });
                endpoints.MapGet("/int", async context => {
                    var collection = context.RequestServices.GetService<ICollection<int>>();
                    collection.Add(collection.Count() + 1);
                    foreach (var val in collection)
                    {
                        await context.Response.WriteAsync($"Int: {val}\n");
                    }
                });
            });
        }
    }
}
