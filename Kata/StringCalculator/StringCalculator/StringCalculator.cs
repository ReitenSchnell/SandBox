using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace StringCalculator
{
    public class CalculatorTests
    {
        private readonly Calculator calculator = new Calculator();

        [Fact]
        public void Add_EmptyString_Returns0()
        {
            var result = calculator.Add(String.Empty);
            result.Should().Be(0);
        }

        [Fact]
        public void Add_OneNumber_ReturnsThisNumber()
        {
            var result = calculator.Add("1");
            result.Should().Be(1);
        }

        [Fact]
        public void Add_TwoValues_ReturnsTheirSum()
        {
            var result = calculator.Add("1,2");
            result.Should().Be(3);
        }

        [Fact]
        public void Add_UnknownAmountOfValues_ReturnsTheirSum()
        {
            var rand = new Random();
            var values = Enumerable.Range(0, rand.Next(1, 100)).Select(val => rand.Next(1, 100)).ToArray();
            var result = calculator.Add(string.Join(",", values));
            var expected = 0;
            values.ToList().ForEach(val => expected += val);
            result.Should().Be(expected);
        }

        [Fact]
        public void Add_NewLineSeparator_ReturnsSum()
        {
            var result = calculator.Add("1\n2,3");
            result.Should().Be(6);
        }

        [Fact]
        public void Add_OneCharSeparator_ReturnsSum()
        {
            var result = calculator.Add("//;\n2;3;4");
            result.Should().Be(9);
        }
    }

    public class Calculator
    {
        public int Add(string number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;
            var separators = new[] { ",", "\n" };
            if (number.StartsWith("//"))
            {
                var newSeparator = number.Substring(2, 1);
                return
                    number.Substring(5).Split(new[] {newSeparator}, StringSplitOptions.RemoveEmptyEntries).Sum(val => Int32.Parse(val));
            }
            return (number.Split(separators, StringSplitOptions.RemoveEmptyEntries)).Sum(val => Int32.Parse(val));
        }
    }
}