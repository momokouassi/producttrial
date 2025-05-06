namespace ProductTrial.Services.Middlewares.ExceptionHandler.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}