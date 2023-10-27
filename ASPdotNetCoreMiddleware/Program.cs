using ASPdotNetCoreMiddleware.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
// register the Custom Middleware class as a service
builder.Services.AddTransient<MyCustomMiddleware>();  
var app = builder.Build();

// use Use() extension method to excute a non terminating middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello1, and ");
    await next(context);
});

// use Use() extension method to excute a non terminating middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello2 again. ");
    await next(context);
});

// register the customer middleware class - MyCustomMiddleware
//app.UseMiddleware<MyCustomMiddleware>();
// call extension method from Custom middleware class
//app.UseMiddleware<MyCustomMiddleware>(); 

// call the extension method create by item template
app.UseHelloCustomMiddleware();


// UseWhen()
app.UseWhen(
    context => context.Request.Query.ContainsKey("username"), 
    app =>
    {
        app.Use(async (context, next) => 
        {
            await context.Response.WriteAsync("Hello from Middleware UseWhen branch\n");
            await next(context);
        });
    }
    );
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from Middleware Chain");

});

// use Run() extension method to execute a terminating middleware
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello3 from a teminating mdw\n");
    
});

app.Run();
