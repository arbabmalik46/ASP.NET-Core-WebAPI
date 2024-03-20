using MyFirstApp;
using Microsoft.AspNetCore.WebUtilities;
using System.Reflection.Metadata.Ecma335;

//Returns an instance of WebApplicationsBuilder class
var builder = WebApplication.CreateBuilder(args);
List<Information> Information = new List<Information>();
//Returns an instance of WebApplication
var app = builder.Build();
//Creating a route
app.MapGet("/", (HttpContext context) =>
    {
        string a = context.Request.Headers["Accept"].ToString();
        string agent = context.Request.Headers["User-Agent"].ToString();
        context.Response.Headers["Content-Type"] = "text/html";
        context.Response.Headers["MyHeader"] = "ToonPiX";
        return "<h2>This is a reponse application</h2>"+ "Check Type:" + a + "\nCheck user agent:" + agent;
    }
);
app.Run(async (HttpContext context) => {
    string path = context.Request.Path;
    string method = context.Request.Method;
    string id = "";
    string product = "";
    if (path == "/")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Server has started");
    }
    else if (path == "/Home")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Hello from the Home Page");
    }
    else if (path == "/Contact")
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            foreach (var item in Information)
            {
                await context.Response.WriteAsync($"The ID is {item.ID} and Product name is {item.Name}\n");
            }
        }
        else if (method == "POST")
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";
            Information information1 = new Information();
            information1.ID = Convert.ToInt32(context.Request.Query["id"]);
            information1.Name = context.Request.Query["name"].ToString();
            Information.Add(information1);
            await context.Response.WriteAsync("Record Added");
        }
        else if (method == "DELETE") 
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";
            int tempID = Convert.ToInt32(context.Request.Query["id"]);
            Information.RemoveAll(x => x.ID == tempID);
            await context.Response.WriteAsync("Record Deleted");
        }
        
        //if (context.Request.Query.ContainsKey("id"))
        //{
        //    id = context.Request.Query["id"].ToString();
        //}
        //if (context.Request.Query.ContainsKey("name"))
        //{
        //    product = context.Request.Query["name"].ToString();
        //}
    }
    else if (method =="GET" && path == "/Products")
    {
        if(context.Request.Query.ContainsKey("id"))
        {
            id = context.Request.Query["id"].ToString();
        }
        if(context.Request.Query.ContainsKey("name"))
        {
            product = context.Request.Query["name"].ToString();
        }
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync($"Hello from the Products server. You have selected {product} which is based on id: {id}");
    }
    else if (method == "POST" && path == "/Products")
    {
        StreamReader reader = new StreamReader(context.Request.Body);
        string data = await reader.ReadToEndAsync();
        await context.Response.WriteAsync($"Body contains: {data}");
        var objectdata = QueryHelpers.ParseQuery(data);
        
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"{path.Remove(0,1)} not found");
    }
});
//Starting the server
app.Run();
