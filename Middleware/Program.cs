using Middleware.CustomMiddleware;

//1. Create an instance of WebApplication Builder
var builder = WebApplication.CreateBuilder(args);

//Creating a custom middleware
builder.Services.AddTransient<MyMiddleware>();
//2. Using that webapplication builder, now we are creating an instance of Webapplication
var app = builder.Build();

app.Use(async(HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("Hello World, This is a simple ASP.Net Application");
    await next(context);
});
app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("\nThis is my first middleware");
    await next(context);
});
//app.UseMiddleware<MyMiddleware>();
//app.MyMiddleware();
app.UseMyConventionalMiddleware();

app.UseWhen(context => context.Request.Query.ContainsKey("IsAuthorized"), app => {
    app.Use(async (HttpContext context,RequestDelegate next) =>
    {
        await context.Response.WriteAsync("This is my conditional middleware 4");
        await next(context);
    });
});
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("\nThis is my second middleware");
});
//Custom Middleware
app.Run();