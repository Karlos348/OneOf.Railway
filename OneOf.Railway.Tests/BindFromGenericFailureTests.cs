using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromGenericFailureTests
{
    [Fact]
    public void ToSuccess()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => ResultFactory.Success());

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var visited = false;
        
        ResultFactory.Failure<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Success();
            });

        Assert.False(visited);
    }
    
    [Fact]
    public void ToGenericSuccess()
    {
        var value = 348;
        var error = "Error";
        
        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => ResultFactory.Success(value));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToGenericSuccess_ShouldNotInvokeNextDelegate()
    {
        var value = 348;
        var error = "Error";
        var visited = false;
        
        ResultFactory.Failure<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Success(value);
            });

        Assert.False(visited);
    }
    
    [Fact]
    public void ToFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => ResultFactory.Failure("x"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var visited = false;
        
        ResultFactory.Failure<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Failure("x");
            });

        Assert.False(visited);
    }
    
    [Fact]
    public void ToGenericFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => ResultFactory.Failure<int>("x"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToGenericFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var visited = false;
        
        ResultFactory.Failure<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Failure<int>("x");
            });

        Assert.False(visited);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(_ => Helper.SuccessAsync());

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var visited = false;
        
        await ResultFactory.Failure<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.SuccessAsync();
            });

        Assert.False(visited);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var error = "Error";
        var value = 348;
        
        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(_ => Helper.SuccessAsync(value));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var value = 348;
        var visited = false;
        
        await ResultFactory.Failure<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.SuccessAsync(value);
            });

        Assert.False(visited);
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(_ => Helper.FailureAsync("another_error"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var visited = false;
        
        await ResultFactory.Failure<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.FailureAsync("another_error");
            });

        Assert.False(visited);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(_ => Helper.FailureAsync<int>("another_error"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var visited = false;
        
        await ResultFactory.Failure<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.FailureAsync<int>("another_error");
            });

        Assert.False(visited);
    }
}