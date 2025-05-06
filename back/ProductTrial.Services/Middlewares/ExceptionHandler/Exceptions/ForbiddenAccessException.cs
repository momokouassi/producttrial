namespace ProductTrial.Services.Middlewares.ExceptionHandler.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string? message) : base(message)
        {
        }
    }
}