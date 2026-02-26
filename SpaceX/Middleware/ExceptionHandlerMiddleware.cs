using System.Net.Mime;
using System.Text.Json;

namespace SpaceX.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string errorCode = "InternalError";
                string errorMessage = ex.Message;
                string? StackTrace = ex.StackTrace;

                _logger.LogError(ex, $"{errorCode}: {errorMessage}");

                context.Response.ContentType = MediaTypeNames.Application.Json;

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                string json;

                var error = new
                {
                    Code = 500,
                    Description = errorMessage,
                    StackTrace = StackTrace,
                };
                json = JsonSerializer.Serialize(error, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
