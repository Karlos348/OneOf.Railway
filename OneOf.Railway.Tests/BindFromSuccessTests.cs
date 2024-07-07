using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromSuccessTests
{
    [Fact]
    public void ToSuccess()
    {
        var result = ResultFactory.Success()
            .Bind(ResultFactory.Success);

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void ToGenericSuccess()
    {
        var value = 348;
        
        var result = ResultFactory.Success()
            .Bind(() => ResultFactory.Success(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public void ToFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Success()
            .Bind(() => ResultFactory.Failure(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public void ToGenericFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Success()
            .Bind(() => ResultFactory.Failure<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var result = await ResultFactory.Success()
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var value = 348;
        
        var result = await ResultFactory.Success()
            .Bind(() => Helper.SuccessAsync(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Success()
            .Bind(() => Helper.FailureAsync(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Success()
            .Bind(() => Helper.FailureAsync<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
}