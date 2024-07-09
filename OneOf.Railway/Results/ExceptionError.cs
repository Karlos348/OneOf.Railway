namespace OneOf.Railway.Results;

public class ExceptionError : Failure
{
    public ExceptionError(Exception exception, string? errorMessage = null) : base("CORE_EXCEPTION")
    {
        if (exception is null)
        {
            throw new ArgumentException($"{nameof(Exception)} cannot be null");
        }

        ErrorMessage = errorMessage ?? exception.Message;
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
    }
    
    public string ErrorMessage { get; }
    public Exception Exception { get; }
}