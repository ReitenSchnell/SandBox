using System;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem2
    {
        public int GetFibonacciEvenTerms(int maxValue)
        {
            var sumEvenValued = 0;
            var prevMember = 1;
            var member = 2;
            while (member < maxValue)
            {
                if (member % 2 == 0)
                    sumEvenValued += member;
                var temp = member;
                member += prevMember;
                prevMember = temp;
            }
            return sumEvenValued;
        }
    }

    public class Problem2Tests
    {
        private readonly Problem2 problem = new Problem2();

        [Fact]
        public void GetFibonacciEvenTerms_CheckResult()
        {
            var result = problem.GetFibonacciEvenTerms(100);
            result.Should().Be(44);
        }

        [Fact]
        public void GetFibonacciEvenTerms_Main_CheckResult()
        {
            var result = problem.GetFibonacciEvenTerms(4000000);
            result.Should().Be(4613732);
        }
    }

}