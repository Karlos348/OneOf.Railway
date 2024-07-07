using Xunit;

namespace OneOf.Railway.Tests;

public class BindFromFailureTests
{
    [Fact]
    public void ToSuccess()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() =>
            {
                throw new InvalidOperationException();
                return ResultFactory.Success();
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public void ToGenericSuccess()
    {
        var value = 348;
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() =>
            {
                throw new InvalidOperationException();
                return ResultFactory.Success(value);
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public void ToFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() =>
            {
                throw new InvalidOperationException();
                return ResultFactory.Failure("x");
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public void ToGenericFailure()
    {
        var error = "Error";
        
        var result = ResultFactory.Failure(error)
            .Bind(() =>
            {
                throw new InvalidOperationException();
                return ResultFactory.Failure<int>("x");
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncSuccess()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                throw new InvalidOperationException();
                return await Helper.SuccessAsync();
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncGenericSuccess()
    {
        var error = "Error";
        var value = 348;
        
        var result = await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                throw new InvalidOperationException();
                return await Helper.SuccessAsync(value);
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }

    [Fact]
    public async Task ToAsyncFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                throw new InvalidOperationException();
                return await Helper.FailureAsync("another_error");
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
    
    [Fact]
    public async Task ToAsyncGenericFailure()
    {
        var error = "Error";
        
        var result = await ResultFactory.Failure(error)
            .Bind(async () =>
            {
                throw new InvalidOperationException();
                return await Helper.FailureAsync<int>("another_error");
            });

        Assert.True(result.IsFailure);
        Assert.Equal(error, result.GetFailure().GlobalCode);
    }
}