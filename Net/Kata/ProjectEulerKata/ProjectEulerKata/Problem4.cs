using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem4
    {
        public long GetLargestPalindrom(int digits)
        {
            var max = (int)Math.Pow(10, digits);
            var min = (int)Math.Pow(10, digits - 1);
            var list = new List<long>();
            for (var i = max -1; i >= min; i--)
            {
                for (var j = max - 1; j >= min; j--)
                {
                    var prod = i*j;
                    if (IsPalindrom(prod))
                        list.Add(prod); 
                }
            }
            return list.Max();
        }

        public bool IsPalindrom(long number)
        {
            var numberStr = number.ToString();
            var charArray = numberStr.ToCharArray();
            Array.Reverse(charArray);
            return numberStr == String.Join("", charArray);
        }
    }

    public class Problem4Tests
    {
        private readonly Problem4 problem = new Problem4();

        [Fact]
        public void GetLargestPalindrom_TwoDigits_Return9009()
        {
            var result = problem.GetLargestPalindrom(2);
            result.Should().Be(9009);
        }

        [Fact]
        public void GetLargestPalindrom_ThreeDigits_Return9009()
        {
            var result = problem.GetLargestPalindrom(3);
            result.Should().Be(906609);
        }

        [Fact]
        public void Test()
        {
            Assert.Equal(1000, Math.Pow(10,3));
        }

        [Fact]
        public void IsPalindrom_CheckTrueResult()
        {
            var result = problem.IsPalindrom(23455432);
            result.Should().BeTrue();
        }

        [Fact]
        public void IsPalindrom_CheckFalseResult()
        {
            var result = problem.IsPalindrom(23455431);
            result.Should().BeFalse();
        }
    }
}