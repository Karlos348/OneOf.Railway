using OneOf.Types;

namespace OneOf.Railway.Results;

public class Result : OneOfBase<Success, Failure>
{
    private Result(OneOf<Success, Failure> _) : base(_) { }
    
    public bool IsSuccess => IsT0;
    public bool IsFailure => IsT1;

    public static implicit operator Result(Success _) => new (_);
    public static implicit operator Result(Failure _) => new (_);
}

public class Result<T> : OneOfBase<Success<T>, Failure>
{
    private Result(OneOf<Success<T>, Failure> _) : base(_) { }

    public bool IsSuccess => IsT0;
    public bool IsFailure => IsT1;
    
    public static implicit operator Result<T>(Success<T> _) => new (_);
    public static implicit operator Result<T>(Failure _) => new (_);
}