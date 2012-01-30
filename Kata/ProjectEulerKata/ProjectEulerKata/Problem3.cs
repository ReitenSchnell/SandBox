using System;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem3
    {
        public long FindLargestPrime(long number)
        {
            var devider = 2;
            while (number > devider)
            {
                if (number % devider != 0)
                    devider++;
                else
                    number /= devider;
            }
            return number;
        }
    }

    public class Problem3Tests
    {
        private readonly Problem3 problem3 = new Problem3();

        [Fact]
        public void FindLargestPrime_CheckResult()
        {
            var result = problem3.FindLargestPrime(13195);
            result.Should().Be(29);
        }

        [Fact]
        public void FindLargestPrime_MainTest()
        {
            var result = problem3.FindLargestPrime(600851475143);
            result.Should().Be(6857);
        }
    }
}