using OneOf.Railway.Results;
using OneOf.Types;

namespace OneOf.Railway;

public static class ResultFactory
{
    private static readonly Result SuccessShared = new Success();

    public static Result Success() => SuccessShared;
    public static Results.Result<TResult> Success<TResult>(TResult result) => new Success<TResult>(result);
    public static Results.Result<TResult> Failure<TResult>(string code) => new Failure(code);
    public static Result Failure(string code) => new Failure(code);
}