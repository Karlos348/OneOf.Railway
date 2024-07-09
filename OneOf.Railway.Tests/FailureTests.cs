using OneOf.Railway.Results;
using Xunit;

namespace OneOf.Railway.Tests;

public class FailureTests
{
    [Fact]
    public void ShouldThrowArgumentException_WhenGlobalCodeIsNull()
    {
        Assert.Throws<ArgumentException>(() => new Failure(null));
    }
    
    [Fact]
    public void ShouldThrowArgumentException_WhenGlobalCodeIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Failure(string.Empty));
        Assert.Throws<ArgumentException>(() => new Failure(" "));
    }

    [Fact]
    public void ShouldSetGlobalCodeCorrectly()
    {
        var globalCode = "ERROR_CODE";

        var failure = new Failure(globalCode);

        Assert.Equal(globalCode, failure.GlobalCode);
    }

    [Fact]
    public void IsValidationFailure_ShouldBeTrue_WhenGlobalCodeIsValidationCode()
    {
        var failure = new Failure("CORE_VALIDATION");

        Assert.True(failure.IsValidationFailure);
    }

    [Fact]
    public void IsValidationFailure_ShouldBeFalse_WhenGlobalCodeIsValidationCode()
    {
        var failure = new Failure("NOT_VALIDATION_CODE");
        
        Assert.False(failure.IsValidationFailure);
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameGlobalCode()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var areEqual = failure1.Equals(failure2);

        Assert.True(areEqual);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenGlobalCodesAreDifferent()
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
    public void GetHashCode_ShouldReturnSameHashCode_ForSameGlobalCode()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var hash1 = failure1.GetHashCode();
        var hash2 = failure2.GetHashCode();

        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnDifferentHashCodes_WhenGlobalCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var hash1 = failure1.GetHashCode();
        var hash2 = failure2.GetHashCode();

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnTrue_ForSameGlobalCode()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var areEqual = failure1 == failure2;

        Assert.True(areEqual);
    }

    [Fact]
    public void OperatorNotEquals_ShouldReturnFalse_ForSameGlobalCode()
    {
        var failure1 = new Failure("ERROR_CODE");
        var failure2 = new Failure("ERROR_CODE");

        var areNotEqual = failure1 != failure2;

        Assert.False(areNotEqual);
    }

    [Fact]
    public void OperatorEquals_ShouldReturnFalse_WhenGlobalCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var areEqual = failure1 == failure2;

        Assert.False(areEqual);
    }

    [Fact]
    public void OperatorNotEquals_ShouldReturnTrue_WhenGlobalCodesAreDifferent()
    {
        var failure1 = new Failure("ERROR_CODE_1");
        var failure2 = new Failure("ERROR_CODE_2");

        var areNotEqual = failure1 != failure2;

        Assert.True(areNotEqual);
    }
}