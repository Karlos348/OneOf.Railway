using Xunit;

namespace OneOf.Railway.Tests;

public class TryGetValueTests
{
    [Fact]
    public void ShouldReturnTrueAndSetValue_WhenSuccess()
    {
        var expectedValue = 348;
        var globalResult = ResultFactory.Success(expectedValue);

        var success = globalResult.TryGetValue(out var value);

        Assert.True(success);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void ShouldReturnFalseAndSetValueToDefault_WhenFailure()
    {
        var globalResult = ResultFactory.Failure<int>("Error");
        
        var success = globalResult.TryGetValue(out var value);
        
        Assert.False(success);
        Assert.Equal(default, value);
    }
}