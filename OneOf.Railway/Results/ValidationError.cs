namespace OneOf.Railway.Results;

public class ValidationError : Failure
{
    public ValidationError(params string[] codes) : base("CORE_VALIDATION")
    {
        if (codes == null || codes.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException($"{nameof(Codes)} cannot be null or empty");
        }
        Codes = codes;
    }
    
    public string[] Codes { get; }
}