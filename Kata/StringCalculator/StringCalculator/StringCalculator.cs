using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string number)
        {
            if (String.IsNullOrEmpty(number))
                return 0;
            const string pattern = @"(?<val>\d+)";
            var values = (from Match m in new Regex(pattern).Matches(number) where m.Success select Int32.Parse(m.Groups["val"].Value)).ToList();
            return values.Sum();
        }
    }

    public class StringCalculatorTests
    {
        private readonly StringCalculator calculator = new StringCalculator();

        private int Act(string input)
        {
            return calculator.Add(input);
        }

        [Fact]
        public void Add_NullValue_Returns0()
        {
            var result = Act(null);
            result.Should().Be(0);
        }

        [Fact]
        public void Add_EmptyString_Returns0()
        {
            var result = Act(String.Empty);
            result.Should().Be(0);
        }

        [Fact]
        public void Add_OnValue_ReturnsThisValue()
        {
            var result = Act("7");
            result.Should().Be(7);
        }

        [Fact]
        public void Add_TwoValues_ReturnsTheirSum()
        {
            var result = Act("2,3");
            result.Should().Be(5);
        }

        [Fact]
        public void Add_RandomAmountOfValues_ReturnsTheirSum()
        {
            var rand = new Random();
            var values = Enumerable.Range(0, rand.Next(0, 100)).ToArray();
            var number = string.Join(",", values);
            var expected = values.Aggregate((a, b) => a + b);
            var result = calculator.Add(number);
            result.Should().Be(expected);
        }

        [Fact]
        public void Add_NewLineSeparator_ReturnsSum()
        {
            var result = calculator.Add("2\n3,4\n1");
            result.Should().Be(10);
        }
    }
}