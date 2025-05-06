using Microsoft.AspNetCore.Http;

namespace ProductTrial.Services.Middlewares.ExceptionHandler
{
    public interface IErrorHandlerMiddleware
    {
        Task HandleException(HttpContext context, Exception exception, int? statusCode = null);
    }
}