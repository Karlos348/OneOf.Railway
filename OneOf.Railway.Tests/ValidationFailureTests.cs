using OneOf.Railway.Results;
using Xunit;

namespace OneOf.Railway.Tests;

public class ValidationFailureTests
{
    [Fact]
    public void ShouldInheritFromFailure()
    {
        var validationError = new ValidationFailure("CODE1", "CODE2");

        Assert.IsAssignableFrom<Failure>(validationError);
    }

    [Fact]
    public void ShouldSetCodeAsToValidationCode()
    {
        var validationError = new ValidationFailure("CODE1");

        Assert.Equal(ValidationFailure.BaseCode, validationError.Code);
    }

    [Fact]
    public void ShouldSetCodesCorrectly()
    {
        var codes = new[] { "CODE1", "CODE2", "CODE3" };

        var validationError = new ValidationFailure(codes);

        Assert.Equal(codes, validationError.Codes);
    }

    [Fact]
    public void ShouldBeValidationFailure()
    {
        var validationError = new ValidationFailure("CODE1");

        Assert.True(validationError.IsValidationFailure);
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenCodesContainNull()
    {
        Assert.Throws<ArgumentException>(() => new ValidationFailure("CODE1", null));
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenCodesContainEmptyString()
    {
        Assert.Throws<ArgumentException>(() => new ValidationFailure("CODE1", string.Empty));
        Assert.Throws<ArgumentException>(() => new ValidationFailure("CODE1", " "));
    }
}