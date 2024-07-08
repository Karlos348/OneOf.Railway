using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromAsyncGenericSuccessTests
{
    [Fact]
    public async Task ToSuccess()
    {
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => ResultFactory.Success());

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task ToSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync(value)
            .Bind(ResultFactory.Success);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToGenericSuccess()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => ResultFactory.Success(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToGenericSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync(value)
            .Bind(ResultFactory.Success);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => ResultFactory.Failure(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToGenericFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => ResultFactory.Failure<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => Helper.SuccessAsync());

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task ToAsyncSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync(value)
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => Helper.SuccessAsync(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = await Helper.SuccessAsync(value)
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => Helper.FailureAsync(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await Helper.SuccessAsync("x")
            .Bind(_ => Helper.FailureAsync<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
}