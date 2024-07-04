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
}