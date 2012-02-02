using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem5
    {
        public long GetMinimumValue(int max)
        {
            var deviders = new List<int>();
            var numbers = Enumerable.Range(1, max).Where(val => val>1).ToList();
            numbers.ForEach(d =>
                                 {
                                     var currentDevs = FindDividers(d);
                                     ArrangeSets(deviders, currentDevs);
                                 });
            return deviders.Aggregate((a,b) => b*a);
        }

        public void ArrangeSets(List<int> mainset, List<int> set)
        {
            mainset.Sort();
            set.Sort();
            if (!mainset.Any())
                mainset.AddRange(set);
            for (var i = 0; i < set.Count; i++)
            {
                if (mainset[i] != set[i])
                    mainset.Insert(i, set[i]);
            }
        }

        public List<int> FindDividers(int number)
        {
            var devider = 2;
            var result = new List<int>();
            while (number > devider)
            {
                if (number % devider != 0)
                    devider++;
                else
                {
                    number /= devider;
                    result.Add(devider);
                }
            }
            result.Add(number);
            return result;
        }
    }

    public class Problem5Tests
    {
        private readonly Problem5 problem5 = new Problem5(); 

        [Fact]
        public void GetFactorial_CheckResult()
        {
            var result = problem5.GetMinimumValue(10);
            result.Should().Be(2520);
        }

        [Fact]
        public void FindDividers_2_CheckResult()
        {
            var result = problem5.FindDividers(2);
            result.Should().BeEquivalentTo(new List<int> {2});
        }

        [Fact]
        public void FindDividers_9_CheckResult()
        {
            var result = problem5.FindDividers(9);
            result.Should().BeEquivalentTo(new List<int> { 3, 3 });
        }

        [Fact]
        public void FindDividers_6_CheckResult()
        {
            var result = problem5.FindDividers(6);
            result.Should().BeEquivalentTo(new List<int> { 2, 3 });
        }

        [Fact]
        public void ArrangeSets_CheckResult()
        {
            var set = new List<int> {2, 2, 3, 5};
            var mainset = new List<int> {2, 5};
            problem5.ArrangeSets(mainset, set);
            mainset.Should().BeEquivalentTo(new List<int> {2, 2, 3, 5});
        }

        [Fact]
        public void ArrangeSets_CheckEmptySet()
        {
            var set = new List<int> { 2, 2, 3, 5 };
            var mainset = new List<int> ();
            problem5.ArrangeSets(mainset, set);
            mainset.Should().BeEquivalentTo(new List<int> { 2, 2, 3, 5 });
        }
    }
}