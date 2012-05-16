using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem6
    {
        public long GetSquareDiff(int maxValue)
        {
            var numbers = Enumerable.Range(1, maxValue).ToList();
            var squareSum = (long)Math.Pow(numbers.Sum(), 2);
            var sumOfSquares = numbers.Aggregate((a, b) => a + b*b);
            return Math.Abs(squareSum - sumOfSquares);
        }
    }

    public class Problem6Tests
    {
        private readonly Problem6 problem = new Problem6();

        [Fact]
        public void GetSquareDiff_CheckResult()
        {
            var result = problem.GetSquareDiff(10);
            result.Should().Be(2640);
        }

        [Fact]
        public void GetSquareDiff_CheckFinalResult()
        {
            var result = problem.GetSquareDiff(100);
            result.Should().Be(25164150);
        }
    }
}