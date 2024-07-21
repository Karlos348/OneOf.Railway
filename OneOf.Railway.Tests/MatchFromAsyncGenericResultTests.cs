using Xunit;

namespace OneOf.Railway.Tests;

public class MatchFromAsyncGenericResultTests
{
    [Fact]
    public async Task ShouldReturnSuccessResult()
    {
        var successResult = "Success";
        var value = 348;
        var successTask = Helper.SuccessAsync(value);
        
        var result = await successTask.Match(
            _ => successResult, 
            _ => "x");

        Assert.Equal(successResult, result);
    }
    
    [Fact]
    public async Task ShouldPassResultToSuccessScope()
    {
        var value = 348;
        var successTask = Helper.SuccessAsync(value);
        
        var result = await successTask.Match(
            successValue => successValue, 
            _ => throw new NotImplementedException());

        Assert.Equal(value, result);
    }

    [Fact]
    public async Task ShouldReturnFailureResult()
    {
        var failureResult = "Failure";
        var failureTask = Helper.FailureAsync<int>("Error");
        
        var result = await failureTask.Match(
            _ => "x", 
            _ => failureResult);
        
        Assert.Equal(failureResult, result);
    }
    
    [Fact]
    public async Task ShouldPassFailureToFailureScope()
    {
        var errorCode = "Error";
        var failureTask = Helper.FailureAsync<int>(errorCode);
        
        var result = await failureTask.Match(
            _ => "x", 
            failure => failure.Code);
        
        Assert.Equal(errorCode, result);
    }
}