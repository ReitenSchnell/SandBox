using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem5
    {
        private readonly List<int> dividers = new List<int>();

        public long GetMinimumValue(int max)
        {
            var numbers = Enumerable.Range(1, max).Where(val => val>1).ToList();
            numbers.ForEach(FindDividers);
            return dividers.Aggregate((a,b) => b*a);
        }
       
        public void FindDividers(int number)
        {
            foreach (var i in dividers)
            {
                if (number%i != 0) continue;
                number = number/i;
            }
            if (number != 1)
                dividers.Add(number);
        }
    }

    public class Problem5Tests
    {
        private readonly Problem5 problem5 = new Problem5(); 

        [Fact]
        public void GetMinimumValue_CheckResult()
        {
            var result = problem5.GetMinimumValue(10);
            result.Should().Be(2520);
        }

        [Fact]
        public void GetMinimumValue_CheckFinalResult()
        {
            var result = problem5.GetMinimumValue(20);
            result.Should().Be(232792560);
        }
    }
}