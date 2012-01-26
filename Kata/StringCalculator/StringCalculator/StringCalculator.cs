using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace StringCalculator
{
    public class CalculatorTests
    {
        private readonly Calculator calculator = new Calculator();

        [Fact]
        public void Add_GetsEmptyString_Returns0()
        {
            int result = calculator.Add(String.Empty);
            result.Should().Be(0);
        }

        [Fact]
        public void Add_GetsTwoNumbers_ReturnsSumOfThem()
        {
            int result = calculator.Add("1,5");
            result.Should().Be(6);
        }

        [Fact]
        public void Add_GetsThreeNumbers_ReturnsSumOfThem()
        {
            int result = calculator.Add("1,5,8");
            result.Should().Be(14);
        }

        [Fact]
        public void Add_GetsRandomAmountOfNumbers_ReturnsSumOfThem()
        {
            var rand = new Random();
            int[] numbers = Enumerable.Range(0, rand.Next(0, 100)).Select(val => rand.Next(0, 100)).ToArray();
            int result = calculator.Add(string.Join(",", numbers));
            result.Should().Be(numbers.Sum());
        }

        [Fact]
        public void Add_SeparatorIsNewLine_ReturnsSum()
        {
            int result = calculator.Add("1,2\n3");
            result.Should().Be(6);
        }

        [Fact]
        public void Add_SeparatorIsNotDefaultAndOneChar_ReturnsSum()
        {
            var result = calculator.Add("//;\n1;2;3");
            result.Should().Be(6);
        }

        [Fact]
        public void Add_SeparatorIsNotDefaultAndTwoChars_ReturnsSum()
        {
            var result = calculator.Add("//;;\n1;;2;;3");
            result.Should().Be(6);
        }

        [Fact]
        public void Add_OneNegativeValue_Throws()
        {
            calculator.Invoking(c => c.Add("//;;\n1;;-2;;3")).ShouldThrow<Exception>().WithMessage("negatives not allowed: -2");
        }

        [Fact]
        public void Add_NegativeValues_ThrowsAndMessageContainsAllNegativeValues()
        {
            calculator.Invoking(c => c.Add("//;;\n1;;-2;;-3")).ShouldThrow<Exception>().WithMessage("negatives not allowed: -2, -3");
        }
    }

    public class Calculator
    {
        public int Add(string s)
        {
            if (String.IsNullOrEmpty(s)) return 0;

            const string markOfBeginning = "//";
            const char newLine = '\n';
            string newSeparator = null;
            if (s.StartsWith(markOfBeginning) && s.IndexOf(newLine) > 0)
            {
                newSeparator = s.Substring(markOfBeginning.Length, s.IndexOf(newLine) - markOfBeginning.Length);
                s = s.Substring(s.IndexOf(newLine));
            }
            var values = newSeparator == null
                                  ? s.Split(new[] {',', newLine})
                                  : s.Split(new[] {newSeparator}, StringSplitOptions.RemoveEmptyEntries);
            if (values.Any(v => v.StartsWith("-")))
            {
                var negatives = values.Where(val => val.StartsWith("-"));
                throw new Exception(String.Format("negatives not allowed: {0}", string.Join(", ", negatives)));
            }
            return values.ToList().Select(Int32.Parse).Sum();
        }
    }
}