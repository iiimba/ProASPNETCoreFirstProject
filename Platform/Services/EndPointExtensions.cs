using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace Platform.Services
{
    public static class EndpointExtensions
    {
        public static void MapEndpoint<T>(this IEndpointRouteBuilder app, string path, string methodName = "Endpoint")
        {
            var methodInfo = typeof(T).GetMethod(methodName);
            if (methodInfo == null || methodInfo.ReturnType != typeof(Task))
            {
                throw new Exception("Method cannot be used");
            }

            //T endpointInstance = ActivatorUtilities.CreateInstance<T>(app.ServiceProvider);

            var methodParams = methodInfo.GetParameters();
            app.MapGet(path, context =>
            {
                T endpointInstance = ActivatorUtilities.CreateInstance<T>(context.RequestServices);
                return (Task)methodInfo.Invoke(
                    endpointInstance,
                    methodParams.Select(p => p.ParameterType == typeof(HttpContext)
                        ? context
                        : context.RequestServices.GetService(p.ParameterType)).ToArray());

                //return (Task)methodInfo.Invoke(
                //    endpointInstance,
                //    methodParams.Select(p => p.ParameterType == typeof(HttpContext)
                //        ? context
                //        : context.RequestServices.GetService(p.ParameterType)).ToArray());
            });
        }
    }
}
