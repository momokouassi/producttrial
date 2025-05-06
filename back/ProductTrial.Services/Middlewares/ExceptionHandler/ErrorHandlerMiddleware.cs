using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductTrial.Services.Middlewares.ExceptionHandler.Exceptions;
using System.Text.Json;

namespace ProductTrial.Services.Middlewares.ExceptionHandler
{
    public class ErrorHandlerMiddleware : IErrorHandlerMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task HandleException(HttpContext context, Exception exception, int? statusCode = null)
        {
            _logger.LogError(exception, exception.Message);

            var errorMessage = new
            {
                error = exception.Message
            };

            string payload = JsonSerializer.Serialize(errorMessage);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode ?? context.Response.StatusCode;
            await context.Response.WriteAsync(payload);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AlreadyExistsException ex)
            {
                await HandleException(context, ex, StatusCodes.Status409Conflict);
            }
            catch (ForbiddenAccessException ex)
            {
                await HandleException(context, ex, StatusCodes.Status403Forbidden);
            }
            catch (NotFoundException ex)
            {
                await HandleException(context, ex, StatusCodes.Status204NoContent);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleException(context, ex, StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(ex.Message, ex);
                _logger.LogError(ex, "The system has encountered an internal error. We apologize for the inconvenience.");
                await HandleException(context, exception);
            }
        }
    }
}