using Xunit;

namespace OneOf.Railway.Tests;

public class ResultFactoryTests
{
    [Fact]
    public void ShouldReturnSharedSuccessInstance()
    {
        var result1 = ResultFactory.Success();
        var result2 = ResultFactory.Success();

        Assert.Same(result1, result2);
        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
    }

    [Fact]
    public void ShouldReturnSuccessWithResult()
    {
        var expectedResult = "AEZAKMI";

        var result = ResultFactory.Success(expectedResult);

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedResult, result.GetValue());
    }

    [Fact]
    public void ShouldReturnFailureWithProperCode()
    {
        var error = "Error123";

        var result = ResultFactory.Failure(error);

        Assert.True(result.IsFailure);
        Assert.Equal(error,
            result.GetFailure().Code);
    }

    [Fact]
    public void GenericResultShouldReturnFailureWithProperCode()
    {
        var error = "Error456";

        var result = ResultFactory.Failure<string>(error);

        Assert.True(result.IsFailure);
        Assert.Equal(error,
            result.Match(_ => throw new InvalidOperationException(), failure => failure.Code));
    }
}