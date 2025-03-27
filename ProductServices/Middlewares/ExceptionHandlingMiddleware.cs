using Products.BusinessLayer.Exceptions;
using ProductServices.DTOs.Application;

namespace ProductServices.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message, exception.InnerException);
            var response = exception switch
            {
                ApplicationException _ => new ExceptionResponseDto("Bad Request", exception.Message),
                EntityNotFoundException _ => new ExceptionResponseDto("Not Found", exception.Message),
                UnauthorizedAccessException _ => new ExceptionResponseDto("Unauthorized user", exception.Message),
                _ => new ExceptionResponseDto("Internal Server error", exception.Message)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
