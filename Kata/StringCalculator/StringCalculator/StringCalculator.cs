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
        public void Add_OneValue_ReturnsThisValue()
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
            var numbers = Enumerable.Range(0, rand.Next(1, 100)).Select(x => rand.Next(1, 100)).ToList();
            var expected = 0;
            numbers.ForEach(num => expected += num);
            var result = calculator.Add(string.Join(",", numbers.ToArray()));
            result.Should().Be(expected);
        }

        [Fact]
        public void Add_NewLineSeparator_ReturnsSum()
        {
            var result = calculator.Add("1,2\n3");
            result.Should().Be(6);
        }

        [Fact]
        public void Add_OneCharDelimiter_ReturnsSum()
        {
            var result = calculator.Add("//;\n1;2;3");
            result.Should().Be(6);
        }
    }

    public class Calculator
    {
        public int Add(string number)
        {
            if (String.IsNullOrEmpty(number))
                return 0;
            const string mark = "//";
            if (number.StartsWith(mark))
            {
                var newSeparator = number[2];
                number = number.Substring(4);
                return number.Split(new[] {newSeparator}).Sum(val => Int32.Parse(val));
            }
            var defaultSeparators = new[] {",", "\n"};
            return number.Split(defaultSeparators, StringSplitOptions.RemoveEmptyEntries).Sum(val => Int32.Parse(val));
        }
    }
}