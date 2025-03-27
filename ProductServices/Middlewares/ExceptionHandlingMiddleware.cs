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
                ApplicationException _ => new ExceptionResponseDto("Bad Request", exception.Message, StatusCodes.Status400BadRequest),
                EntityNotFoundException _ => new ExceptionResponseDto("Not Found", exception.Message, StatusCodes.Status404NotFound),
                UnauthorizedAccessException _ => new ExceptionResponseDto("Unauthorized user", exception.Message, StatusCodes.Status401Unauthorized),
                _ => new ExceptionResponseDto("Internal Server error", exception.Message, StatusCodes.Status500InternalServerError)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
