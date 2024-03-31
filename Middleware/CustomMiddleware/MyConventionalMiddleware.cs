using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public MyConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.WriteAsync("\nThis is our convetional Middleware, before next method");
            await _next(httpContext);
            httpContext.Response.WriteAsync("\nThis is our convetional Middleware after next method");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyConventionalMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyConventionalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyConventionalMiddleware>();
        }
    }
}
