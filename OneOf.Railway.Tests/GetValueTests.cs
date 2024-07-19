using Xunit;

namespace OneOf.Railway.Tests;

public class GetValueTests
{
    [Fact]
    public void ShouldReturnValue_WhenSuccess()
    {
        var expectedValue = 348;
        var globalResult = ResultFactory.Success(expectedValue);

        var result = OneOfGlobalResultExtensions.GetValue(globalResult);

        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public void ShouldThrowInvalidOperationException_WhenFailure()
    {
        var globalResult = ResultFactory.Failure<int>("Error");
        
        Assert.Throws<InvalidOperationException>(() => OneOfGlobalResultExtensions.GetValue(globalResult));
    }
}