using OneOf.Types;

namespace OneOf.Railway.Results;

public class Result : OneOfBase<Success, Failure>
{
    private Result(OneOf<Success, Failure> _) : base(_) { }
    
    public bool IsSuccess => IsT0;
    public bool IsFailure => IsT1;

    public Result Bind(Func<Result> f)
    {
        return Match(
            _ => f.Invoke(),
            _ => this);
    }

    public Result<TResult> Bind<TResult>(Func<Result<TResult>> f)
    {
        return Match<Result<TResult>>(
            _ => f.Invoke(),
            failure => failure);
    }

    public async Task<Result> Bind(Func<Task<Result>> f)
    {
        return await Match<Task<Result>>(
            _ => f.Invoke(),
            _ => Task.FromResult(this));
    }

    public async Task<Result<TResult>> Bind<TResult>(Func<Task<Result<TResult>>> f)
    {
        return await Match<Task<Result<TResult>>>(
            _ => f.Invoke(),
            failure => Task.FromResult((Result<TResult>)failure));
    }
    
    public static implicit operator Result(Success _) => new (_);
    public static implicit operator Result(Failure _) => new (_);
}

public class Result<T> : OneOfBase<Success<T>, Failure>
{
    private Result(OneOf<Success<T>, Failure> _) : base(_) { }

    public bool IsSuccess => IsT0;
    public bool IsFailure => IsT1;
    
    public Result Bind(Func<T, Result> f)
    {
        return Match(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public Result<TResult> Bind<TResult>(Func<T, Result<TResult>> f)
    {
        return Match<Result<TResult>>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public async Task<Result> Bind(Func<T, Task<Result>> f)
    {
        return await Match(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((Result)failure));
    }
    
    public async Task<Result<TResult>> Bind<TResult>(Func<T, Task<Result<TResult>>> f)
    {
        return await Match(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((Result<TResult>)failure));
    }
    
    public bool TryGetValue(out T value) 
    {
        var (success, fetchedResult) = Match(
            x => (true, x.Value),
            _ => (false, default)!);
        
        value = fetchedResult;
        return success;
    }
    
    public T GetValue()
    {
        return Match(
            x => x.Value, 
            _ => throw new InvalidOperationException());
    }
    
    public static implicit operator Result<T>(Success<T> _) => new (_);
    public static implicit operator Result<T>(Failure _) => new (_);
}