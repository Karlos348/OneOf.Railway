using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromAsyncSuccessTests
{
    [Fact]
    public async Task ToSuccess()
    {
        var result = await Helper.SuccessAsync()
            .Bind(ResultFactory.Success);

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task ToGenericSuccess()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync()
            .Bind(() => ResultFactory.Success(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync()
            .Bind(() => ResultFactory.Failure(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToGenericFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync()
            .Bind(() => ResultFactory.Failure<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var result = await Helper.SuccessAsync()
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync()
            .Bind(() => Helper.SuccessAsync(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync()
            .Bind(() => Helper.FailureAsync(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync()
            .Bind(() => Helper.FailureAsync<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
}