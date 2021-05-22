using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Platform
{
    public class Capital
    {
        public static async Task Endpoint(HttpContext context)
        {
            string capital = null;
            var country = context.Request.RouteValues["country"] as string;
            switch ((country ?? string.Empty).ToLower())
            {
                case "uk":
                    capital = "London";
                    break;
                case "france":
                    capital = "Paris";
                    break;
                case "monaco":
                    var generator = context.RequestServices.GetService<LinkGenerator>();
                    var url = generator.GetPathByRouteValues(context, "population", new { city = country });
                    context.Response.Redirect(url);
                    return;
            }
            if (capital != null)
            {
                await context.Response.WriteAsync($"{capital} is the capital of {country}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
