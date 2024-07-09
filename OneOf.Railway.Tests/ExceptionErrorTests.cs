using OneOf.Railway.Results;
using Xunit;

namespace OneOf.Railway.Tests;

public class ExceptionErrorTests
{
    [Fact]
    public void ShouldInheritFromFailure()
    {
        var exceptionError = new ExceptionError(new InvalidOperationException(), "msg");

        Assert.IsAssignableFrom<Failure>(exceptionError);
    }

    [Fact]
    public void ShouldSetGlobalCodeExceptionCode()
    {
        var exceptionError = new ExceptionError(new InvalidOperationException(), "msg");

        Assert.Equal("CORE_EXCEPTION", exceptionError.GlobalCode);
    }

    [Fact]
    public void ShouldSetErrorMessageCorrectly()
    {
        var errorMessage = "Error message";

        var exceptionError = new ExceptionError(new InvalidOperationException(), errorMessage);

        Assert.Equal(errorMessage, exceptionError.ErrorMessage);
    }

    [Fact]
    public void ShouldSetExceptionCorrectly()
    {
        var exception = new InvalidOperationException();

        var exceptionError = new ExceptionError(exception, "msg");

        Assert.Equal(exception, exceptionError.Exception);
    }

    [Fact]
    public void ShouldUseExceptionMessage_WhenErrorMessageIsNotGiven()
    {
        var exception = new InvalidOperationException("Exception message");

        var exceptionError = new ExceptionError(exception, null);

        Assert.Equal(exception.Message, exceptionError.ErrorMessage);
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenExceptionIsNull()
    {
        Assert.Throws<ArgumentException>(() => new ExceptionError(null, "msg"));
    }
}