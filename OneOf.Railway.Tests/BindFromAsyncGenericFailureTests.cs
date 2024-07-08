using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromAsyncGenericFailureTests
{
    [Fact]
    public async Task ToSuccess()
    {
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Success();
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToGenericSuccess()
    {
        var value = 348;
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Success(value);
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToFailure()
    {
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Failure("x");
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToGenericFailure()
    {
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(_ =>
            {
                visited = true;
                return ResultFactory.Failure<int>("x");
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToAsyncSuccess()
    {
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.SuccessAsync();
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var error = "Error";
        var value = 348;
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.SuccessAsync(value);
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.FailureAsync("another_error");
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        var visited = false;

        var result = await Helper.FailureAsync<decimal>(error)
            .Bind(async _ =>
            {
                visited = true;
                return await Helper.FailureAsync<int>("another_error");
            });

        Assert.True(result.IsFailure);
        Assert.False(visited);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
}