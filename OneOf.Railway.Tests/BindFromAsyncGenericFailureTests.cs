using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromAsyncGenericFailureTests
{
    [Fact]
    public async Task ToSuccess()
    {
        var error = "Error";

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => ResultFactory.Success());

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                invoked = true;
                return ResultFactory.Success();
            });

        Assert.False(invoked);
    }

    [Fact]
    public async Task ToGenericSuccess()
    {
        var value = 348;
        var error = "Error";

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => ResultFactory.Success(value));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToGenericSuccess_ShouldNotInvokeNextDelegate()
    {
        var value = 348;
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                invoked = true;
                return ResultFactory.Success(value);
            });

        Assert.False(invoked);
    }

    [Fact]
    public async Task ToFailure()
    {
        var error = "Error";

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => ResultFactory.Failure("x"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                invoked = true;
                return ResultFactory.Failure("x");
            });

        Assert.False(invoked);
    }

    [Fact]
    public async Task ToGenericFailure()
    {
        var error = "Error";

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => ResultFactory.Failure<int>("x"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToGenericFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
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

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => Helper.SuccessAsync());

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
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

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => Helper.SuccessAsync(value));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var value = 348;
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
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

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => Helper.FailureAsync("another_error"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
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

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ => Helper.FailureAsync<int>("another_error"));

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure_ShouldNotInvokeNextDelegate()
    {
        var error = "Error";
        var invoked = false;

        await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
            {
                invoked = true;
                return await Helper.FailureAsync<int>("another_error");
            });

        Assert.False(invoked);
    }
}