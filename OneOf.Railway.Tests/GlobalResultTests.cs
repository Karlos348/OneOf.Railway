using OneOf.Railway.Results;
using OneOf.Types;
using Xunit;

namespace OneOf.Railway.Tests;

public class GlobalResultTests
{
    [Fact]
    public void ShouldBeSuccess_WhenConstructedWithSuccess()
    {
        var success = new Success();
        
        GlobalResult result = success;
        
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
    }

    [Fact]
    public void ShouldBeFailure_WhenConstructedWithFailure()
    {
        var error = "Error";
        var failure = new Failure(error);
        
        GlobalResult result = failure;
        
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
}