using Xunit;

namespace OneOf.Railway.Tests;

public class MatchFromAsyncResultTests
{
    [Fact]
    public async Task ShouldReturnSuccessResult()
    {
        var successResult = "Success";
        var successTask = Helper.SuccessAsync();
        
        var result = await successTask.Match(
            () => successResult, 
            _ => "x");

        Assert.Equal(successResult, result);
    }

    [Fact]
    public async Task ShouldReturnFailureResult()
    {
        var failureResult = "Failure";
        var failureTask = Helper.FailureAsync("Error");
        
        var result = await failureTask.Match(
            () => "x", 
            _ => failureResult);
        
        Assert.Equal(failureResult, result);
    }
    
    [Fact]
    public async Task ShouldPassFailureToFailureScope()
    {
        var errorCode = "Error";
        var failureTask = Helper.FailureAsync(errorCode);
        
        var result = await failureTask.Match(
            () => "x", 
            failure => failure.Code);
        
        Assert.Equal(errorCode, result);
    }
}