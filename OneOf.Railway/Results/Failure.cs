namespace OneOf.Railway.Results;

public class Failure : IEquatable<Failure>
{
    public string Code { get; private set; }

    public Failure(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException($"{nameof(code)} cannot be null or empty");
        }
        Code = code;
    }

    public bool IsValidationFailure => Code == ValidationFailure.BaseCode;

    public override bool Equals(object? obj) => obj is Failure other && Equals(other);

    public bool Equals(Failure p) => Code == p.Code;

    public override int GetHashCode() => Code.GetHashCode();

    public static bool operator ==(Failure f1, Failure f2) => f1.Equals(f2);

    public static bool operator !=(Failure f1, Failure f2) => !(f1 == f2);
}