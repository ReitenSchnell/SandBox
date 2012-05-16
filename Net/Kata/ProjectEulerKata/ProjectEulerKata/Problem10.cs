using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem10
    {
        public long GetSumPrimeBelow(int val)
        {
            var primes = new List<long>{2};
            var numbers = Enumerable.Range(3, val-3).ToList();
            numbers.ForEach(number =>
                                {
                                    var isPrime = primes.All(prime => number%prime != 0);
                                    if (isPrime)
                                        primes.Add(number);
                                });
            return primes.Sum();
        }

    }

    public class Problem10Tests
    {
        private readonly Problem10 problem = new Problem10();

        [Fact]
        public void GetSumPrimeBelow_CheckResult()
        {
            var result = problem.GetSumPrimeBelow(10);
            result.Should().Be(17);
        }

        [Fact]
        public void GetSumPrimeBelow_CheckFinalResult()
        {
            var result = problem.GetSumPrimeBelow(2000000);
            result.Should().Be(142913828922);
        }
    }
}