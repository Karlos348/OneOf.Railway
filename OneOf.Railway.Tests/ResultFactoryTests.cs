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
    public void ShouldReturnFailureWithGlobalId()
    {
        var globalCode = "Error123";

        var result = ResultFactory.Failure(globalCode);

        Assert.True(result.IsFailure);
        Assert.Equal(globalCode,
            result.GetFailure().GlobalCode);
    }

    [Fact]
    public void GenericResultShouldReturnFailureWithGlobalId()
    {
        var globalCode = "Error456";

        var result = ResultFactory.Failure<string>(globalCode);

        Assert.True(result.IsFailure);
        Assert.Equal(globalCode,
            result.Match(_ => throw new InvalidOperationException(), failure => failure.GlobalCode));
    }
}