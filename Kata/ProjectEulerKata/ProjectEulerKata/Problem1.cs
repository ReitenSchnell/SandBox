using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;

namespace ProjectEulerKata
{
    public class Problem1
    {
        public int GetNaturalSum(int i)
        {
            return Enumerable.Range(0, i).Where(val => val%3 == 0 || val%5 == 0).Sum();
        }
    }

    public class Problem1Tests
    {
        private readonly Problem1 problem1 = new Problem1();

        [Fact]
        public void GetNaturalSum_10_CheckResult()
        {
            var result = problem1.GetNaturalSum(10);
            result.Should().Be(23);
        }

        [Fact]
        public void GetNaturalSum_1000_CheckResult()
        {
            var result = problem1.GetNaturalSum(1000);
            result.Should().Be(233168);
        }
    }
}
