using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace PrimaryNumbers
{
    public class Splitter
    {
        public int[] Split(int number)
        {
            if (number <= 0)
                throw new Exception("Number must be positive");
            var result = new List<int>();
            for (var i = 2; i <= number; i++)
            {
                var isPrimary = true;
                for (var j = 2; j < i; j++)
                {
                    if (i%j != 0) continue;
                    isPrimary = false;
                    break;
                }
                if (isPrimary && number%i == 0)
                {
                    while (number % i ==0)
                    {
                        result.Add(i);
                        number = number / i;
                    }
                }
            }
            return result.ToArray();
        }
    }

    public class SplitterTests
    {
        private readonly Splitter splitter = new Splitter();

        [Fact]
        public void Split_NumberIsNegative_ThrowsWrongNumber()
        {
            splitter.Invoking(s => s.Split(-420)).ShouldThrow<Exception>().WithMessage("Number must be positive");
        }

        [Fact]
        public void Split_NumberIs0_ThrowsWrongNumber()
        {
            splitter.Invoking(s => s.Split(0)).ShouldThrow<Exception>().WithMessage("Number must be positive");
        }

        [Fact]
        public void Split_CheckResult()
        {
            var result = splitter.Split(420);
            result.Should().BeEquivalentTo(2, 2, 3, 5, 7);
        }

        [Fact]
        public void Split_2_CheckResult()
        {
            var result = splitter.Split(2);
            result.Should().BeEquivalentTo(2);
        }

        [Fact]
        public void Split_3_CheckResult()
        {
            var result = splitter.Split(3);
            result.Should().BeEquivalentTo(3);
        }

        [Fact]
        public void Split_17340_CheckResult()
        {
            var result = splitter.Split(17340);
            result.Should().BeEquivalentTo(2,2,3,5,17,17);
        }
    }
}