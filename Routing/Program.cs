using Routing;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    //Define Routes
    endpoints.Map("/",HelperMethods.HelloWorld);

});


app.Run();
