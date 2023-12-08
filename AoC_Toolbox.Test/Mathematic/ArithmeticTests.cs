using AoC_Toolbox.Mathematic;
using FluentAssertions;

namespace AoC_Toolbox.Test.Mathematic;

public class ArithmeticTests
{
    [Theory]
    [Trait("Type", "Unittest")]
    [InlineData(4, 6, 12, "Basic Case")]
    [InlineData(0, 5, 0, "Zero Input")]
    [InlineData(15, 25, 75, "Common Factors")]
    [InlineData(7, 13, 91, "Prime Numbers")]
    [InlineData(18, 24, 72, "Common Factors with Larger Numbers")]
    [InlineData(1, 1, 1, "Identity Case")]
    public void LCM_ShouldReturnCorrectResults(long a, long b, long expectedLCM, string becausePhrase)
    {
        // Act
        long result = Arithmetic.LCM(a, b);

        // Assert
        result.Should().Be(expectedLCM, becausePhrase);
    }

    [Theory]
    [Trait("Type", "Unittest")]
    [InlineData(new long[] { 4, 6, 12 }, 12, "Basic Case")]
    [InlineData(new long[] { 2, 3, 5, 7 }, 210, "Prime Numbers")]
    [InlineData(new long[] { 10, 15, 25 }, 150, "Common Factors")]
    [InlineData(new long[] { 0, 5, 10 }, 0, "Zero Input")]
    [InlineData(new long[] { 1, 1, 1, 1 }, 1, "Identity Case")]
    [InlineData(new long[] { 2, 4, 8, 16, 32 }, 32, "Powers of 2")]
    [InlineData(new long[] { 3, 6, 9, 12, 15 }, 180, "Multiples of 3")]
    [InlineData(new long[] { 5, 10, 15, 20, 25 }, 300, "Multiples of 5")]
    [InlineData(new long[] { 7, 14, 21, 28, 35 }, 420, "Multiples of 7")]
    [InlineData(new long[] { 11, 22, 33, 44, 55 }, 660, "Multiples of 11")]
    public void LCM_ShouldReturnCorrectResult(IEnumerable<long> numbers, long expectedLCM, string becausePhrase)
    {
        // Act
        long result = Arithmetic.LCM(numbers);

        // Assert
        result.Should().Be(expectedLCM, becausePhrase);
    }

    [Theory]
    [Trait("Type", "Unittest")]
    [InlineData(4, 6, 2, "Basic Case")]
    [InlineData(7, 14, 7, "Common Factor 7")]
    [InlineData(15, 25, 5, "Common Factor 5")]
    [InlineData(8, 12, 4, "Common Factor 4")]
    [InlineData(9, 18, 9, "Common Factor 9")]
    [InlineData(0, 5, 5, "Zero Input")]
    [InlineData(1, 1, 1, "Identity Case")]
    public void GCD_ShouldReturnCorrectResult(long a, long b, long expectedGCD, string becausePhrase)
    {
        // Act
        long result = Arithmetic.GCD(a, b);

        // Assert
        result.Should().Be(expectedGCD, becausePhrase);
    }

    [Theory]
    [Trait("Type", "Unittest")]
    [InlineData(new long[] { 2, 4, 8, 16, 32 }, 2, "Powers of 2")]
    [InlineData(new long[] { 3, 6, 9, 12, 15 }, 3, "Multiples of 3")]
    [InlineData(new long[] { 5, 10, 15, 20, 25 }, 5, "Multiples of 5")]
    [InlineData(new long[] { 7, 14, 21, 28, 35 }, 7, "Multiples of 7")]
    [InlineData(new long[] { 11, 22, 33, 44, 55 }, 11, "Multiples of 11")]
    [InlineData(new long[] { 2, 3, 5, 7, 11, 13, 17 }, 1, "Coprime Numbers")]
    public void GCD_ShouldReturnCorrectResult_ForIEnumerable(IEnumerable<long> numbers, long expectedGCD, string becausePhrase)
    {
        // Act
        long result = Arithmetic.GCD(numbers);

        // Assert
        result.Should().Be(expectedGCD, becausePhrase);
    }

    [Theory]
    [Trait("Type", "Unittest")]
    [InlineData(new long[] { 4, 6, 8, 16, 32 }, 2, "Powers of 2")]
    public void GetGCD_ShouldReturnCorrectResult(IEnumerable<long> numbers, long expectedGCD, string becausePhrase)
    {
        // Act
        long result = numbers.GetGCD();

        // Assert
        result.Should().Be(expectedGCD, becausePhrase);
    }

    [Theory]
    [Trait("Type", "Unittest")]
    [InlineData(new long[] { 4, 8, 16, 32 }, 32, "Powers of 2")]
    public void GetLCM_ShouldReturnCorrectResult(IEnumerable<long> numbers, long expectedLCM, string becausePhrase)
    {
        // Act
        long result = numbers.GetLCM();

        // Assert
        result.Should().Be(expectedLCM, becausePhrase);
    }
}