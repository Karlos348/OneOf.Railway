using OneOf.Railway.Results;

namespace OneOf.Railway;

public static class ResultExtensions
{
    #region Bind

    #region From Task<Result>

    public static async Task<Result> Bind(this Task<Result> task, Func<Result> f)
    {
        var result = await task;
        return result.Bind(f.Invoke);
    }
    
    public static async Task<Result<TResult>> Bind<TResult>(this Task<Result> task, Func<Result<TResult>> f)
    {
        var result = await task;
        return result.Bind(f.Invoke);
    }
    
    public static async Task<Result> Bind(this Task<Result> task, Func<Task<Result>> f)
    {
        var result = await task;
        return await result.Bind(f.Invoke);
    }

    public static async Task<Result<TResult>> Bind<TResult>(this Task<Result> task, Func<Task<Result<TResult>>> f)
    {
        var result = await task;
        return await result.Bind(f.Invoke);
    }

    #endregion

    #region From Task<Result<TResult>>

    public static async Task<Result> Bind<TPrevResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Result> f)
    {
        var result = await task;
        return result.Bind(f.Invoke);
    }
    
    public static async Task<Result<TResult>> Bind<TPrevResult, TResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Result<TResult>> f)
    {
        var result = await task;
        return result.Bind(f.Invoke);
    }
    
    public static async Task<Result> Bind<TPrevResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Task<Result>> f)
    {
        var result = await task;
        return await result.Bind(f.Invoke);
    }
    
    public static async Task<Result<TResult>> Bind<TPrevResult, TResult>(this Task<Result<TPrevResult>> task, 
        Func<TPrevResult, Task<Result<TResult>>> f)
    {
        var result = await task;
        return await result.Bind(f.Invoke);
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
}