namespace OneOf.Railway.Results;

public class ExceptionFailure : Failure
{
    public static readonly string BaseCode = "CORE_EXCEPTION";

    public ExceptionFailure(Exception exception, string? errorMessage = null) : base(BaseCode)
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