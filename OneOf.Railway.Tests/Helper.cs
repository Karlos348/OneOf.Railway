using OneOf.Railway.Results;

namespace OneOf.Railway.Tests;

public static class Helper
{
    public static TValue GetValue<TValue>(this Result<TValue> result)
    {
        return result.GetValue();
    }
    
    public static Failure GetFailure(this Result result)
    {
        return result.Match(_ => throw new InvalidOperationException("Result is not a failure."), f => f);
    }
    
    public static Failure GetFailure<TValue>(this Result<TValue> result)
    {
        return result.Match(_ => throw new InvalidOperationException("Result is not a failure."), f => f);
    }

    public static Task<Result> SuccessAsync() => Task.FromResult(ResultFactory.Success());
    public static Task<Result<TResult>> SuccessAsync<TResult>(TResult value) => Task.FromResult(ResultFactory.Success(value));
    public static Task<Result> FailureAsync(string error) => Task.FromResult(ResultFactory.Failure(error));
    public static Task<Result<TResult>> FailureAsync<TResult>(string error) => Task.FromResult(ResultFactory.Failure<TResult>(error));
}