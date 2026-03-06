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
    public void IsValidationFailure_ShouldBeTrue_WhenInstanceIsValidationFailure()
    {
        var failure = new ValidationFailure("CODE");

        Assert.True(failure.IsValidationFailure);
    }

    [Fact]
    public void IsValidationFailure_ShouldBeFalse_WhenCodeIsNotValidationCode()
    {
        var failure = new Failure("NOT_VALIDATION_CODE");

        Assert.False(failure.IsValidationFailure);
    }

    [Fact]
    public void IsValidationFailure_ShouldBeFalse_WhenCodeMatchesValidationBaseCode_ButTypeIsNotValidationFailure()
    {
        var failure = new Failure(ValidationFailure.BaseCode);

        Assert.False(failure.IsValidationFailure);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOtherIsNull()
    {
        var failure = new Failure("ERROR_CODE");

        var areEqual = failure.Equals(null);

        Assert.False(areEqual);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnTrue_WhenBothAreNull()
    {
        Failure? f1 = null;
        Failure? f2 = null;

        Assert.True(f1 == f2);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnFalse_WhenLeftIsNull()
    {
        Failure? f1 = null;
        var f2 = new Failure("ERROR_CODE");

        Assert.False(f1 == f2);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnFalse_WhenRightIsNull()
    {
        var f1 = new Failure("ERROR_CODE");
        Failure? f2 = null;

        Assert.False(f1 == f2);
    }

    [Fact]
    public void OperatorNotEquals_ShouldReturnTrue_WhenLeftIsNull()
    {
        Failure? f1 = null;
        var f2 = new Failure("ERROR_CODE");

        Assert.True(f1 != f2);
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

    [Fact]
    public void Equals_ShouldReturnTrue_WhenFailureAndValidationFailureHaveSameCode()
    {
        var failure = new Failure(ValidationFailure.BaseCode);
        var validationFailure = new ValidationFailure("CODE1");

        Assert.True(failure.Equals(validationFailure));
        Assert.True(failure == validationFailure);
    }
}