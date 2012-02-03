using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem7
    {
        private readonly List<long> primes = new List<long>();

        public long FindNthPrime(int n)
        {
            var number = 2;
            while (primes.Count < n)
            {
                ExtendPrimeList(number);
                number++;
            }
            return primes.Last();
        }

        private void ExtendPrimeList(long number)
        {
            var i = 0;
            while (i < primes.Count)
            {
                if (number % primes[i] != 0)
                    i++;
                else
                    number /= primes[i];
            }
            if (number != 1)
                primes.Add(number);
        }
    }

    public class Problem7Tests
    {
        private readonly Problem7 problem = new Problem7();

        [Fact]
        public void Fing6thPrime_Returns13()
        {
            var result = problem.FindNthPrime(6);
            result.Should().Be(13);
        }

        [Fact]
        public void Fing10001thPrime_Returns13()
        {
            var result = problem.FindNthPrime(10001);
            result.Should().Be(104743);
        }
    }
}