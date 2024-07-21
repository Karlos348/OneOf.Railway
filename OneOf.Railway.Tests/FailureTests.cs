using OneOf.Railway.Results;
using Xunit;

namespace OneOf.Railway.Tests;

public class FailureTests
{
    [Fact]
    public void ShouldThrowArgumentException_WhenCodeIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Failure(null));
    }
    
    [Fact]
    public void ShouldThrowArgumentException_WhenCodeIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Failure(string.Empty));
        Assert.Throws<ArgumentException>(() => new Failure(" "));
    }

    [Fact]
    public void ShouldSetCodeCorrectly()
    {
        var code = "ERROR_CODE";

        var failure = new Failure(code);

        Assert.Equal(code, failure.Code);
    }

    [Fact]
    public void IsValidationFailure_ShouldBeTrue_WhenCodeIsValidationCode()
    {
        var failure = new Failure(ValidationFailure.BaseCode);

        Assert.True(failure.IsValidationFailure);
    }

    [Fact]
    public void IsValidationFailure_ShouldBeFalse_WhenCodeIsValidationCode()
    {
        var failure = new Failure("NOT_VALIDATION_CODE");
        
        Assert.False(failure.IsValidationFailure);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameCodes()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var areEqual = failure1.Equals(failure2);

        Assert.True(areEqual);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var areEqual = failure1.Equals(failure2);

        Assert.False(areEqual);
    }
    
    [Fact]
    public void Equals_ShouldReturnFalse_WhenObjectIsNotFailureInstance()
    {
        var failure1 = new Failure("ERROR_CODE");

        var areEqual = failure1.Equals(new {});

        Assert.False(areEqual);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameHashCode_ForSameCodes()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var hash1 = failure1.GetHashCode();
        var hash2 = failure2.GetHashCode();

        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentHashCodes_WhenCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var hash1 = failure1.GetHashCode();
        var hash2 = failure2.GetHashCode();

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnTrue_ForSameCodes()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var areEqual = failure1 == failure2;

        Assert.True(areEqual);
    }

    [Fact]
    public void OperatorNotEquals_ShouldReturnFalse_ForSameCodes()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var areNotEqual = failure1 != failure2;

        Assert.False(areNotEqual);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnFalse_WhenCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var areEqual = failure1 == failure2;

        Assert.False(areEqual);
    }

    [Fact]
    public void OperatorNotEquals_ShouldReturnTrue_WhenCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var areNotEqual = failure1 != failure2;

        Assert.True(areNotEqual);
    }
}