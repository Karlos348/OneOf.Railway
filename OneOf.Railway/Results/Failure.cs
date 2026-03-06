namespace OneOf.Railway.Results;

public class Failure : IEquatable<Failure>
{
    public string Code { get; init; }

    public Failure(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException($"{nameof(code)} cannot be null or empty");
        }
        Code = code;
    }

    public bool IsValidationFailure => this is ValidationFailure;

    public override bool Equals(object? obj) => obj is Failure other && Equals(other);

    public bool Equals(Failure? p) => p is not null && Code == p.Code;

    public override int GetHashCode() => Code.GetHashCode();

    public static bool operator ==(Failure? f1, Failure? f2) => f1?.Equals(f2) ?? f2 is null;

    public static bool operator !=(Failure? f1, Failure? f2) => !(f1 == f2);
}