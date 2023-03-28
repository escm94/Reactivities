using System.Net;
using System.Text.Json;
using Application.Core;

namespace API.Middleware
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
      _env = env;
      _logger = logger;
      _next = next;

    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex, ex.Message);
            // doing this because we're not actually in the context of an API controller
            // our API controller is going to default to returning our response as application JSON, but when we're
            // outside of an API controller, we need to specify this information.
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment() 
                ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) 
                : new AppException(context.Response.StatusCode, "Internal Server Error");

            var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
  }
}