namespace OneOf.Railway.Results;

public class ValidationFailure : Failure
{
    public static readonly string BaseCode = "CORE_VALIDATION";

    public ValidationFailure(params string[] codes) : base(BaseCode)
    {
        if (codes == null || codes.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException($"{nameof(codes)} cannot be null or empty");
        }
        Codes = codes;
    }
    
    public string[] Codes { get; }
}