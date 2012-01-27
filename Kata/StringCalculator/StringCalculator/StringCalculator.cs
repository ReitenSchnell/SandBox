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
            var result =calculator.Add(String.Empty);
            result.Should().Be(0);
        }

        [Fact]
        public void Add_OneNumber_RetursNumber()
        {
            var result = calculator.Add("3");
            result.Should().Be(3);
        }

        [Fact]
        public void AddTwoNumbers_ReturnsSumOfThem()
        {
            var result = calculator.Add("2,3");
            result.Should().Be(5);
        }

        [Fact]
        public void Add_ThreeNumbers_ReturnsSumOfThem()
        {
            var result = calculator.Add("2,3,4");
            result.Should().Be(9);
        }

        [Fact]
        public void Add_RandomAmountOfNumbers_ReturnsSumOfThem()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var values = Enumerable.Range(0, rand.Next(1, 1000)).Select(value => rand.Next(0, 1000)).ToArray(); ;
            var result = calculator.Add(String.Join(",", values));
            result.Should().Be(values.Sum());
        }

        [Fact]
        public void Add_SeparatorIsNewLine_ReturnsSum()
        {
            var result = calculator.Add(String.Format("2{0}3", "\n"));
            result.Should().Be(5);
        }

        [Fact]
        public void Add_FirstLineContainsNonDefaultSeparator_ReturnsSum()
        {
            var result = calculator.Add("//;;\n2;;3;;4");
            result.Should().Be(9);
        }
    }

    public class Calculator
    {
        public int Add(string number)
        {
            if (String.IsNullOrEmpty(number))
                return 0;
            const string newLine = "\n";
            var defaultSeparators = new[] { ",", newLine };
            const string separatorMark = "//";
            if (number.StartsWith(separatorMark))
            {
                var newSeparator = number.Substring(separatorMark.Length, number.IndexOf(newLine) - separatorMark.Length);
                number = number.Substring(number.IndexOf(newLine) + 1);
                return number.Split(new[] {newSeparator}, StringSplitOptions.RemoveEmptyEntries).Sum(val => Int32.Parse(val));
            }
            return number.Split(defaultSeparators, StringSplitOptions.RemoveEmptyEntries).Sum(value => Int32.Parse(value));
        }
    }
}