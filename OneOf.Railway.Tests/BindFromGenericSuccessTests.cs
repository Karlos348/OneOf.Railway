using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromGenericSuccessTests
{
    [Fact]
    public void ToSuccess()
    {
        var result = ResultFactory.Success("x")
            .Bind(_ => ResultFactory.Success());

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void ToSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = ResultFactory.Success(value)
            .Bind(ResultFactory.Success);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public void ToGenericSuccess()
    {
        var value = 348;
        
        var result = ResultFactory.Success("x")
            .Bind(_ => ResultFactory.Success(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public void ToGenericSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = ResultFactory.Success(value)
            .Bind(ResultFactory.Success);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public void ToFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Success("x")
            .Bind(_ => ResultFactory.Failure(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToGenericFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Success("x")
            .Bind(_ => ResultFactory.Failure<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var result = await ResultFactory.Success("x")
            .Bind(_ => Helper.SuccessAsync());

        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task ToAsyncSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = await ResultFactory.Success(value)
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var value = 348;
        
        var result = await ResultFactory.Success("x")
            .Bind(_ => Helper.SuccessAsync(value));

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess_ShouldPassTheValue()
    {
        var value = 348;
        
        var result = await ResultFactory.Success(value)
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.GetValue());
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Success("x")
            .Bind(_ => Helper.FailureAsync(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Success("x")
            .Bind(_ => Helper.FailureAsync<int>(error));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
}