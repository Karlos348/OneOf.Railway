using Xunit;

namespace OneOf.Railway.Tests;

public class GetValueTests
{
    [Fact]
    public void ShouldReturnValue_WhenSuccess()
    {
        var expectedValue = 348;
        var success = ResultFactory.Success(expectedValue);

        var result = success.GetValue();

        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public void ShouldThrowInvalidOperationException_WhenFailure()
    {
        var failure = ResultFactory.Failure<int>("Error");
        
        Assert.Throws<InvalidOperationException>(() => failure.GetValue());
    }
}