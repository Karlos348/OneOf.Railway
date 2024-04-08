namespace OneOf.Railway.Results;

public class ExceptionError : Failure
{
    public ExceptionError(string errorMessage, Exception exception) : base("CORE_EXCEPTION")
    {
        ErrorMessage = errorMessage;
        Exception = exception;
    }
    
    public string ErrorMessage { get; }
    public Exception Exception { get; }
}