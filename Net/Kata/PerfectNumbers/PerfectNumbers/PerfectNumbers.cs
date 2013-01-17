using System.Linq;

namespace PerfectNumbers
{
    public class PerfectNumbers
    {
        public bool IsPerfectNumber(int number)
        {
            var dividers = Enumerable.Range(1, number - 1).Where(i => number % i == 0);
            return dividers.Sum() == number;
        }
    }
}
