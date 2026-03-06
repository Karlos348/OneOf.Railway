using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromGenericFailureTests
{
    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public void ToSuccess_ShouldPropagateFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => { invoked = true; return ResultFactory.Success(); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public void ToGenericSuccess_ShouldPropagateFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => { invoked = true; return ResultFactory.Success(348); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public void ToFailure_ShouldPropagateOriginalFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => { invoked = true; return ResultFactory.Failure("x"); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public void ToGenericFailure_ShouldPropagateOriginalFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = ResultFactory.Failure<decimal>(error)
            .Bind(_ => { invoked = true; return ResultFactory.Failure<int>("x"); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public async Task ToAsyncSuccess_ShouldPropagateFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(async _ => { invoked = true; return await Helper.SuccessAsync(); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public async Task ToAsyncGenericSuccess_ShouldPropagateFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(async _ => { invoked = true; return await Helper.SuccessAsync(348); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public async Task ToAsyncFailure_ShouldPropagateOriginalFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(async _ => { invoked = true; return await Helper.FailureAsync("another_error"); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }

    [Theory]
    [InlineData("Error")]
    [InlineData("ANOTHER_ERROR")]
    public async Task ToAsyncGenericFailure_ShouldPropagateOriginalFailureAndNotInvokeDelegate(string error)
    {
        var invoked = false;

        var result = await ResultFactory.Failure<decimal>(error)
            .Bind(async _ => { invoked = true; return await Helper.FailureAsync<int>("another_error"); });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().Code);
        Assert.False(invoked);
    }
}