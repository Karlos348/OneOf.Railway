using OneOf.Railway.Results;

namespace OneOf.Railway;

public static class ResultExtensions
{
    #region Bind

    #region From Result
    
    public static Result Bind(this Result result, Func<Result> f)
    {
        return result.Match(
            _ => f.Invoke(),
            _ => result);
    }
    
    public static Result<TResult> Bind<TResult>(this Result result, Func<Result<TResult>> f)
    {
        return result.Match<Result<TResult>>(
            _ => f.Invoke(),
            failure => failure);
    }
    
    public static async Task<Result> Bind(this Result result, Func<Task<Result>> f)
    {
        return await result.Match<Task<Result>>(
            _ => f.Invoke(),
            _ => Task.FromResult(result));
    }

    public static async Task<Result<TResult>> Bind<TResult>(this Result result, Func<Task<Result<TResult>>> f)
    {
        return await result.Match<Task<Result<TResult>>>(
            _ => f.Invoke(),
            failure => Task.FromResult((Result<TResult>)failure));
    }

    #endregion

    #region From Result<TResult>

    public static Result Bind<TPrevResult>(this Result<TPrevResult> result, Func<TPrevResult, Result> f)
    {
        return result.Match(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static Result<TResult> Bind<TPrevResult, TResult>(this Result<TPrevResult> result, Func<TPrevResult, Result<TResult>> f)
    {
        return result.Match<Result<TResult>>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static async Task<Result> Bind<TPrevResult>(this Result<TPrevResult> result, Func<TPrevResult, Task<Result>> f)
    {
        return await result.Match(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((Result)failure));
    }
    
    public static async Task<Result<TResult>> Bind<TPrevResult, TResult>(this Result<TPrevResult> result, Func<TPrevResult, Task<Result<TResult>>> f)
    {
        return await result.Match(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((Result<TResult>)failure));
    }

    #endregion

    #region From Task<Result>

    public static async Task<Result> Bind(this Task<Result> task, Func<Result> f)
    {
        var result = await task;
        return result.Match<Result>(
            _ => f.Invoke(),
            _ => result);
    }
    
    public static async Task<Result<TResult>> Bind<TResult>(this Task<Result> task, Func<Result<TResult>> f)
    {
        var result = await task;
        return result.Match<Result<TResult>>(
            _ => f.Invoke(),
            failure => failure);
    }
    
    public static async Task<Result> Bind(this Task<Result> task, Func<Task<Result>> f)
    {
        var result = await task;
        return await result.Match<Task<Result>>(
            _ => f.Invoke(),
            _ => Task.FromResult(result));
    }

    public static async Task<Result<TResult>> Bind<TResult>(this Task<Result> task, Func<Task<Result<TResult>>> f)
    {
        var result = await task;
        return await result.Match<Task<Result<TResult>>>(
            _ => f.Invoke(),
            failure => Task.FromResult((Result<TResult>)failure));
    }

    #endregion

    #region From Task<Result<TResult>>

    public static async Task<Result> Bind<TPrevResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Result> f)
    {
        var result = await task;
        return result.Match<Result>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static async Task<Result<TResult>> Bind<TPrevResult, TResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Result<TResult>> f)
    {
        var result = await task;
        return result.Match<Result<TResult>>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static async Task<Result> Bind<TPrevResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Task<Result>> f)
    {
        var result = await task;
        return await result.Match<Task<Result>>(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((Result)failure));
    }
    
    public static async Task<Result<TResult>> Bind<TPrevResult, TResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Task<Result<TResult>>> f)
    {
        var result = await task;
        return await result.Match<Task<Result<TResult>>>(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((Result<TResult>)failure));
    }
    
    #endregion
    
    #endregion
    
    #region Match

    public static async Task<TResult> Match<TResult>(this Task<Result> task,
        Func<TResult> onSuccess, Func<Failure, TResult> onFailure)
    {
        var result = await task;
        return result.Match(_ => onSuccess.Invoke(), onFailure.Invoke);
    }
    
    public static async Task<TResult> Match<TPrevResult, TResult>(this Task<Result<TPrevResult>> task,
        Func<TPrevResult, TResult> onSuccess, Func<Failure, TResult> onFailure)
    {
        var result = await task;
        return result.Match(x => onSuccess.Invoke(x.Value), onFailure);
    }
    
    #endregion
    
    #region GetValue
    
    public static bool TryGetValue<TResult>(this Result<TResult> result, out TResult value) 
    {
        var (success, fetchedResult) = result.Match(
            x => (true, x.Value),
            _ => (false, default)!);
        
        value = fetchedResult;
        return success;
    }
    
    public static TResult GetValue<TResult>(this Result<TResult> result)
    {
        return result.Match(
            x => x.Value, 
            _ => throw new InvalidOperationException());
    }
    
    #endregion
}