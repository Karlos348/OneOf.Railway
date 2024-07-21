using OneOf.Railway.Results;
using OneOf.Types;
using Xunit;

namespace OneOf.Railway.Tests;

public class GenericResultTests
{
    [Fact]
    public void ShouldBeSuccess_WhenConstructedWithSuccess()
    {
        var value = 348;
        var success = new Success<int>(value);

        Results.Result<int> result = success;
        
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(value, result.GetValue());
    }

    [Fact]
    public void ShouldBeFailure_WhenConstructedWithFailure()
    {
        var error = "Error";
        var failure = new Failure(error);

        Results.Result<int> result = failure;
        
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
}