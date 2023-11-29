using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Companies.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Demo
    {
        private readonly RequestDelegate _next;

        public Demo(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Request
            var req = httpContext.Request;

            await _next(httpContext);

            //Response
            var res = httpContext.Response;

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DemoExtensions
    {
        public static IApplicationBuilder UseDemo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Demo>();
        }
    }
}
