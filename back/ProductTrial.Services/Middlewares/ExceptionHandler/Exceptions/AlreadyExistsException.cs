namespace ProductTrial.Services.Middlewares.ExceptionHandler.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string? message) : base(message)
        {
        }
    }
}