using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text.Json;
using WebApp.Models;

namespace WebApp
{
    public static class WebServiceEndpoint
    {
        private static string BASEURL = "api/products";

        public static void MapWebService(this IEndpointRouteBuilder app)
        {
            app.MapGet($"{BASEURL}/{{id}}", async context =>
            {
                var key = long.Parse(context.Request.RouteValues["id"] as string);
                var data = context.RequestServices.GetService<DataContext>();
                var p = data.Products.Find(key);
                if (p == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(p));
                }
            });

            app.MapGet(BASEURL, async context =>
            {
                var data = context.RequestServices.GetService<DataContext>();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
            });

            app.MapPost(BASEURL, async context =>
            {
                var data = context.RequestServices.GetService<DataContext>();
                var p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
                await data.AddAsync(p);
                await data.SaveChangesAsync();
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }
    }
}
