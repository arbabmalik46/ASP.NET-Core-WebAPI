
namespace Middleware.CustomMiddleware
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("\nBefore logic for Middlewares");

            await next(context);
            await context.Response.WriteAsync("\nAfter logic for Middlewares");
        }
    }
    public static class MyExtenionMiddleware
    {
       public static IApplicationBuilder MyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyMiddleware>();
        }
    }
}
