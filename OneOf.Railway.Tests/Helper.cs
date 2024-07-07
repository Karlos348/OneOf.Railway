using OneOf.Railway.Results;

namespace OneOf.Railway.Tests;

public static class Helper
{
    public static TValue GetValue<TValue>(this GlobalResult<TValue> result)
    {
        return result.Match(x => x.Value, _ => throw new InvalidOperationException());
    }
    
    public static Failure GetFailure(this GlobalResult result)
    {
        return result.Match(_ => throw new InvalidOperationException(), f => f);
    }
    
    public static Failure GetFailure<TValue>(this GlobalResult<TValue> result)
    {
        return result.Match(_ => throw new InvalidOperationException(), f => f);
    }

    public static Task<GlobalResult> SuccessAsync() => Task.FromResult(ResultFactory.Success());
    public static Task<GlobalResult<TResult>> SuccessAsync<TResult>(TResult value) => Task.FromResult(ResultFactory.Success(value));
    public static Task<GlobalResult> FailureAsync(string error) => Task.FromResult(ResultFactory.Failure(error));
    public static Task<GlobalResult<TResult>> FailureAsync<TResult>(string error) => Task.FromResult(ResultFactory.Failure<TResult>(error));
}