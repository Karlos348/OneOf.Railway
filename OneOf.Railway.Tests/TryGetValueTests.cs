using Xunit;

namespace OneOf.Railway.Tests;

public class TryGetValueTests
{
    [Fact]
    public void ShouldReturnTrueAndSetValue_WhenSuccess()
    {
        var expectedValue = 348;
        var success = ResultFactory.Success(expectedValue);

        var result = success.TryGetValue(out var value);

        Assert.True(result);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void ShouldReturnFalseAndSetValueToDefault_WhenFailure()
    {
        var failure = ResultFactory.Failure<int>("Error");
        
        var result = failure.TryGetValue(out var value);
        
        Assert.False(result);
        Assert.Equal(default, value);
    }
}