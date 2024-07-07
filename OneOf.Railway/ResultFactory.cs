using OneOf.Railway.Results;
using OneOf.Types;

namespace OneOf.Railway;

public static class ResultFactory
{
    private static readonly GlobalResult SuccessShared = new Success();

    public static GlobalResult Success() => SuccessShared;
    public static GlobalResult<TResult> Success<TResult>(TResult result) => new Success<TResult>(result);
    public static GlobalResult<TResult> Failure<TResult>(string globalCode) => new Failure(globalCode);
    public static GlobalResult Failure(string globalCode) => new Failure(globalCode);
}