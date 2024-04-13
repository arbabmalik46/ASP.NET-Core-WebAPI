using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<HelperClass> lstHelperClass = new List<HelperClass>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/Products/{id}", async (context) =>
    {
        var val = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"The Values on this ID is {lstHelperClass.FirstOrDefault(x=>x.ID == val).ID} and Name is {lstHelperClass.FirstOrDefault(x => x.ID == val).Name}");
    });
    endpoints.MapPost("/Products/{id}/{name}", async (context) => {
        var id =Convert.ToInt32(context.Request.RouteValues["id"]);
        var name =Convert.ToString(context.Request.RouteValues["name"]);
        HelperClass helperClass = new HelperClass
        {
            ID = id,
            Name = name
        };
        lstHelperClass.Add(helperClass);
        await context.Response.WriteAsync("Values Added");
    });
    
    //Define Routes
    //endpoints.Map("/", async (context) =>{
    //    await context.Response.WriteAsync("You are in Root Folder");
    //});

    //endpoints.MapGet("/Home", async (context) => {
    //    await context.Response.WriteAsync("You are in Root Folder With Get Method");
    //});

    //endpoints.MapPost("/Products", async (boo) => {
    //    await boo.Response.WriteAsync("You are in a Post Method");
    //});

});

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("No method is found");
});

app.Run();
