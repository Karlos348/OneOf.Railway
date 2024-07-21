using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromFailureTests
{
    [Fact]
    public void ToSuccess()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(ResultFactory.Success);

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;
        
        ResultFactory.Failure(error)
            .Bind(() =>
            {
                invoked = true;
                return ResultFactory.Success();
            });

        Assert.False(invoked);
    }
    
    [Fact]
    public void ToGenericSuccess()
    {
        var value = 348;
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() => ResultFactory.Success(value));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToGenericSuccess_ShouldNotInvokeNextDelegate()
    {
        var value = 348;
        var error = "Error";
        var invoked = false;
        
        ResultFactory.Failure(error)
            .Bind(() =>
            {
                invoked = true;
                return ResultFactory.Success(value);
            });

        Assert.False(invoked);
    }
    
    [Fact]
    public void ToFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() => ResultFactory.Failure("x"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;
        
        ResultFactory.Failure(error)
            .Bind(() =>
            {
                invoked = true;
                return ResultFactory.Failure("x");
            });

        Assert.False(invoked);
    }
    
    [Fact]
    public void ToGenericFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() => ResultFactory.Failure<int>("x"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public void ToGenericFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;
        
        ResultFactory.Failure(error)
            .Bind(() =>
            {
                invoked = true;
                return ResultFactory.Failure<int>("x");
            });

        Assert.False(invoked);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure(error)
            .Bind(Helper.SuccessAsync);

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;
        
        await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                invoked = true;
                return await Helper.SuccessAsync();
            });

        Assert.False(invoked);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var error = "Error";
        var value = 348;
        
        var result = await ResultFactory.Failure(error)
            .Bind(() => Helper.SuccessAsync(value));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var value = 348;
        var invoked = false;
        
        await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                invoked = true;
                return await Helper.SuccessAsync(value);
            });

        Assert.False(invoked);
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure(error)
            .Bind(() => Helper.FailureAsync("another_error"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;
        
        await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                invoked = true;
                return await Helper.FailureAsync("another_error");
            });

        Assert.False(invoked);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure(error)
            .Bind(() => Helper.FailureAsync<int>("another_error"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;
        
        await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                invoked = true;
                return await Helper.FailureAsync<int>("another_error");
            });

        Assert.False(invoked);
    }
}