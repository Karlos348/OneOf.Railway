using OneOf.Types;

namespace OneOf.Railway.Results;

public class GlobalResult : OneOfBase<Success, Failure>
{
    private GlobalResult(OneOf<Success, Failure> _) : base(_) { }

    public static implicit operator GlobalResult(Success _) => new (_);
    public static implicit operator GlobalResult(Failure _) => new (_);
}

public class GlobalResult<T> : OneOfBase<Success<T>, Failure>
{
    private GlobalResult(OneOf<Success<T>, Failure> _) : base(_) { }
    
    public static implicit operator GlobalResult<T>(Success<T> _) => new (_);
    public static implicit operator GlobalResult<T>(Failure _) => new (_);
}