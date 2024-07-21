using OneOf.Railway.Results;
using Xunit;

namespace OneOf.Railway.Tests;

public class ExceptionFailureTests
{
    [Fact]
    public void ShouldInheritFromFailure()
    {
        var exceptionError = new ExceptionFailure(new InvalidOperationException(), "msg");

        Assert.IsAssignableFrom<Failure>(exceptionError);
    }

    [Fact]
    public void ShouldSetCodeAsExceptionCode()
    {
        var exceptionError = new ExceptionFailure(new InvalidOperationException(), "msg");

        Assert.Equal(ExceptionFailure.BaseCode, exceptionError.Code);
    }

    [Fact]
    public void ShouldSetErrorMessageCorrectly()
    {
        var errorMessage = "Error message";

        var exceptionError = new ExceptionFailure(new InvalidOperationException(), errorMessage);

        Assert.Equal(errorMessage, exceptionError.ErrorMessage);
    }

    [Fact]
    public void ShouldSetExceptionCorrectly()
    {
        var exception = new InvalidOperationException();

        var exceptionError = new ExceptionFailure(exception, "msg");

        Assert.Equal(exception, exceptionError.Exception);
    }

    [Fact]
    public void ShouldUseExceptionMessage_WhenErrorMessageIsNotGiven()
    {
        var exception = new InvalidOperationException("Exception message");

        var exceptionError = new ExceptionFailure(exception, null);

        Assert.Equal(exception.Message, exceptionError.ErrorMessage);
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenExceptionIsNull()
    {
        Assert.Throws<ArgumentException>(() => new ExceptionFailure(null, "msg"));
    }
}