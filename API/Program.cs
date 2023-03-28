using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Persistence;

// CreateBuilder does many things, one of which is create a kestrel server. also reads in the configuration settings specified in 
// appsettings.Development.json and appsettings.json. Development specifies the environment (Development), so when we're developing, 
// this is the one we use.
var builder = WebApplication.CreateBuilder(args);  

// Add services to the container.
// think of services as things we use inside our application logic. we can add services to add more funcitonality to our logic 
// that we create. we'll look into Dependency Injection to inject these services inside other classes within our application.

builder.Services.AddControllers();

// common methodology for keeping Program.cs lean
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();


// this is also referred to as middleware. think of middleware, for now, as things that can do something 
// with the HTTP request on the way in or out. the term pipeline is used for this. so, middleware goes here, services go above
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// gonna remove for now, just to keep things simpler
// app.UseHttpsRedirection();

// this has to be pretty early because right away the browser will send a pre-flight request checking if cors is available
// has to match the name we called it in the service above
app.UseCors("CorsPolicy");

// in the beginning, this won't do much since we don't have authorization/authentication set up at the moment.
app.UseAuthorization();

// this middleware registers the endpoints with the application so that when a request comes in, the app knows where to send it. 
app.MapControllers();

// using - when we're done with this statement, everything inside of the scope variable will be destroyed. 
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;   

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

// this runs the application. 
app.Run();
