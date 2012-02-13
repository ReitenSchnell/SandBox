using System.Linq;
using Xunit;
using FluentAssertions;

namespace ProjectEulerKata
{
    public class Problem9
    {
        public int GetPythagoreanOrtogonal(int sum)
        {
            var a = Enumerable.Range(1, sum/2 + 1).ToList();
            var b = Enumerable.Range(1, sum/2 + 1).ToList();
            return (from vala in a
                        from valb in b
                        where valb*valb + vala*vala == (sum - valb - vala)*(sum - valb - vala)
                        select valb*vala*(sum - valb - vala)).FirstOrDefault();
        }
    }

    public class Problem9Tests
    {
        private readonly Problem9 problem = new Problem9();

        [Fact]
        public void GetPythagoreanOrtogonal_CheckResult()
        {
            var result = problem.GetPythagoreanOrtogonal(12);
            result.Should().Be(60);
        }

        [Fact]
        public void GetPythagoreanOrtogonal_CheckFinalResult()
        {
            var result = problem.GetPythagoreanOrtogonal(1000);
            result.Should().Be(31875000);
        }
    }
}