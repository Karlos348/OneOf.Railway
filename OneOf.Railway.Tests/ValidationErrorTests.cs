using OneOf.Railway.Results;
using Xunit;

namespace OneOf.Railway.Tests;

public class ValidationErrorTests
{
    [Fact]
    public void ShouldInheritFromFailure()
    {
        var validationError = new ValidationError("CODE1", "CODE2");

        Assert.IsAssignableFrom<Failure>(validationError);
    }

    [Fact]
    public void ShouldSetGlobalCodeToValidationCode()
    {
        var validationCode = "CORE_VALIDATION";
        
        var validationError = new ValidationError("CODE1");

        Assert.Equal(validationCode, validationError.GlobalCode);
    }

    [Fact]
    public void ShouldSetCodesCorrectly()
    {
        var codes = new[] { "CODE1", "CODE2", "CODE3" };

        var validationError = new ValidationError(codes);

        Assert.Equal(codes, validationError.Codes);
    }

    [Fact]
    public void ShouldBeValidationFailure()
    {
        var validationError = new ValidationError("CODE1");

        Assert.True(validationError.IsValidationFailure);
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenCodesContainNull()
    {
        Assert.Throws<ArgumentException>(() => new ValidationError("CODE1", null));
    }

    [Fact]
    public void ShouldThrowArgumentException_WhenCodesContainEmptyString()
    {
        Assert.Throws<ArgumentException>(() => new ValidationError("CODE1", string.Empty));
        Assert.Throws<ArgumentException>(() => new ValidationError("CODE1", " "));
    }
}