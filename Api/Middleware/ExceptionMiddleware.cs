using System.Net;
using System.Text.Json;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Ошибка обработки запроса: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = ex switch
            {
                KeyNotFoundException => new { status = (int)HttpStatusCode.NotFound, message = ex.Message },
                ArgumentException => new { status = (int)HttpStatusCode.BadRequest, message = ex.Message },
                _ => new { status = (int)HttpStatusCode.InternalServerError, message = "Внутренняя ошибка сервера" }
            };

            context.Response.StatusCode = response.status;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
