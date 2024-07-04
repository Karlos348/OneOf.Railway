namespace OneOf.Railway.Results;

public class Failure : IEquatable<Failure>
{
    public string GlobalCode { get; private set; }

    public Failure(string globalCode)
    {
        if (string.IsNullOrWhiteSpace(globalCode))
        {
            throw new ArgumentException($"{nameof(globalCode)} cannot be null or empty");
        }
        GlobalCode = globalCode;
    }

    public bool IsValidationFailure => GlobalCode == "CORE_VALIDATION";

    public override bool Equals(object? obj) => obj is Failure other && Equals(other);

    public bool Equals(Failure p) => GlobalCode == p.GlobalCode;

    public override int GetHashCode() => GlobalCode.GetHashCode();

    public static bool operator ==(Failure f1, Failure f2) => f1.Equals(f2);

    public static bool operator !=(Failure f1, Failure f2) => !(f1 == f2);
}