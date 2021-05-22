using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace Platform
{
    public class CountryRouteConstraint : IRouteConstraint
    {
        private static string[] countries = { "uk", "france", "monaco" };

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var segmentValue = values[routeKey] as string ?? string.Empty;
            return Array.IndexOf(countries, segmentValue.ToLower()) > -1;
        }
    }
}
