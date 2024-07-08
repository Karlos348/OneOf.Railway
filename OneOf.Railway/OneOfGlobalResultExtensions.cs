using OneOf.Railway.Results;

namespace OneOf.Railway;

public static class OneOfGlobalResultExtensions
{
    #region Bind

    #region From GlobalResult
    
    public static GlobalResult Bind(this GlobalResult globalResult, Func<GlobalResult> f)
    {
        return globalResult.Match(
            _ => f.Invoke(),
            _ => globalResult);
    }
    
    public static GlobalResult<TResult> Bind<TResult>(this GlobalResult globalResult, Func<GlobalResult<TResult>> f)
    {
        return globalResult.Match<GlobalResult<TResult>>(
            _ => f.Invoke(),
            failure => failure);
    }
    
    public static async Task<GlobalResult> Bind(this GlobalResult globalResult, Func<Task<GlobalResult>> f)
    {
        return await globalResult.Match<Task<GlobalResult>>(
            _ => f.Invoke(),
            _ => Task.FromResult(globalResult));
    }

    public static async Task<GlobalResult<TResult>> Bind<TResult>(this GlobalResult globalResult, Func<Task<GlobalResult<TResult>>> f)
    {
        return await globalResult.Match<Task<GlobalResult<TResult>>>(
            _ => f.Invoke(),
            failure => Task.FromResult((GlobalResult<TResult>)failure));
    }

    #endregion

    #region From GlobalResult<TResult>

    public static GlobalResult Bind<TPrevResult>(this GlobalResult<TPrevResult> globalResult, Func<TPrevResult, GlobalResult> f)
    {
        return globalResult.Match(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static GlobalResult<TResult> Bind<TPrevResult, TResult>(this GlobalResult<TPrevResult> globalResult, Func<TPrevResult, GlobalResult<TResult>> f)
    {
        return globalResult.Match<GlobalResult<TResult>>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static async Task<GlobalResult> Bind<TPrevResult>(this GlobalResult<TPrevResult> globalResult, Func<TPrevResult, Task<GlobalResult>> f)
    {
        return await globalResult.Match(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((GlobalResult)failure));
    }
    
    public static async Task<GlobalResult<TResult>> Bind<TPrevResult, TResult>(this GlobalResult<TPrevResult> globalResult, Func<TPrevResult, Task<GlobalResult<TResult>>> f)
    {
        return await globalResult.Match(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((GlobalResult<TResult>)failure));
    }

    #endregion

    #region From Task<GlobalResult>

    public static async Task<GlobalResult> Bind(this Task<GlobalResult> globalResultTask, Func<GlobalResult> f)
    {
        var globalResult = await globalResultTask;
        return globalResult.Match<GlobalResult>(
            _ => f.Invoke(),
            _ => globalResult);
    }
    
    public static async Task<GlobalResult<TResult>> Bind<TResult>(this Task<GlobalResult> globalResultTask, Func<GlobalResult<TResult>> f)
    {
        var globalResult = await globalResultTask;
        return globalResult.Match<GlobalResult<TResult>>(
            _ => f.Invoke(),
            failure => failure);
    }
    
    public static async Task<GlobalResult> Bind(this Task<GlobalResult> globalResultTask, Func<Task<GlobalResult>> f)
    {
        var globalResult = await globalResultTask;
        return await globalResult.Match<Task<GlobalResult>>(
            _ => f.Invoke(),
            _ => Task.FromResult(globalResult));
    }

    public static async Task<GlobalResult<TResult>> Bind<TResult>(this Task<GlobalResult> globalResultTask, Func<Task<GlobalResult<TResult>>> f)
    {
        var globalResult = await globalResultTask;
        return await globalResult.Match<Task<GlobalResult<TResult>>>(
            _ => f.Invoke(),
            failure => Task.FromResult((GlobalResult<TResult>)failure));
    }

    #endregion

    #region From Task<GlobalResult<TResult>>

    public static async Task<GlobalResult> Bind<TPrevResult>(this Task<GlobalResult<TPrevResult>> globalResultTask, 
        Func<TPrevResult, GlobalResult> f)
    {
        var globalResult = await globalResultTask;
        return globalResult.Match<GlobalResult>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static async Task<GlobalResult<TResult>> Bind<TPrevResult, TResult>(this Task<GlobalResult<TPrevResult>> globalResultTask, 
        Func<TPrevResult, GlobalResult<TResult>> f)
    {
        var globalResult = await globalResultTask;
        return globalResult.Match<GlobalResult<TResult>>(
            success => f.Invoke(success.Value),
            failure => failure);
    }
    
    public static async Task<GlobalResult> Bind<TPrevResult>(this Task<GlobalResult<TPrevResult>> globalResultTask, 
        Func<TPrevResult, Task<GlobalResult>> f)
    {
        var globalResult = await globalResultTask;
        return await globalResult.Match<Task<GlobalResult>>(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((GlobalResult)failure));
    }
    
    public static async Task<GlobalResult<TResult>> Bind<TPrevResult, TResult>(this Task<GlobalResult<TPrevResult>> globalResultTask, 
        Func<TPrevResult, Task<GlobalResult<TResult>>> f)
    {
        var globalResult = await globalResultTask;
        return await globalResult.Match<Task<GlobalResult<TResult>>>(
            success => f.Invoke(success.Value),
            failure => Task.FromResult((GlobalResult<TResult>)failure));
    }
    
    #endregion
    
    #endregion
    
    #region Match

    public static async Task<TResult> Match<TResult>(this Task<GlobalResult> globalResult,
        Func<TResult> onSuccess, Func<Failure, TResult> onFailure)
    {
        var result = await globalResult;
        return result.Match(_ => onSuccess.Invoke(), onFailure.Invoke);
    }
    
    public static async Task<TResult> Match<TPrevResult, TResult>(this Task<GlobalResult<TPrevResult>> globalResult,
        Func<TPrevResult, TResult> onSuccess, Func<Failure, TResult> onFailure)
    {
        var result = await globalResult;
        return result.Match(x => onSuccess.Invoke(x.Value), onFailure);
    }
    
    #endregion
    
    #region GetValue
    
    public static bool TryGetValue<TResult>(this GlobalResult<TResult> globalResult, out TResult value) 
    {
        var (success, result) = globalResult.Match(
            x => (true, x.Value),
            _ => (false, default)!);
        
        value = result;
        return success;
    }
    
    public static TResult GetValue<TResult>(this GlobalResult<TResult> globalResult)
    {
        return globalResult.Match(
            x => x.Value, 
            _ => throw new InvalidOperationException());
    }
    
    #endregion
}