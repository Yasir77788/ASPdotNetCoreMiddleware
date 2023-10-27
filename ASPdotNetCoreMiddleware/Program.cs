var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// use Use() extension method to excute a non terminating middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello, and ");
    await next(context);
});

// use Use() extension method to excute a non terminating middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello again. ");
    await next(context);
});

// use Run() extension method to execute a terminating middleware
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello3 from a teminating mdw");
    
});

app.Run();
