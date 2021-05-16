using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Platform
{
    public class QueryStringMiddleWare
    {
        private readonly RequestDelegate next;

        public QueryStringMiddleWare(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Class-based Middleware \n");
            }
            await next(context);
        }
    }

    public class LocationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly MessageOptions options;

        public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> msgOptions)
        {
            next = nextDelegate;
            options = msgOptions.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{options.CityName}, {options.CountryName}");
            }
            else
            {
                await next(context);
            }
        }
    }
}