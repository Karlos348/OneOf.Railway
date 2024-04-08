namespace OneOf.Railway.Results;

public class ValidationError : Failure
{
    public ValidationError(params string[] codes) : base("CORE_VALIDATION")
    {
        Codes = codes;
    }
    
    public string[] Codes { get; }
}