using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace StringCalculator
{
    public class CalculatorTests
    {
        private readonly Calculator calculator = new Calculator();
        private string inputString;
        private int result;

        private void Act()
        {
            result = calculator.Add(inputString);
        }

        [Fact]
        public void Add_EmptyString_Returns0()
        {
            inputString = String.Empty;
            Act();
            result.Should().Be(0);
        }

        [Fact]
        public void Add_OneValue_ReturnsThisValue()
        {
            inputString = "2";
            Act();
            result.Should().Be(2);
        }

        [Fact]
        public void Add_TwoValues_ReturnsSum()
        {
            inputString = "2,3";
            Act();
            result.Should().Be(5);
        }

        [Fact]
        public void Add_RandomNumberOfValues_ReturnsSum()
        {
            List<int> values = GetValues();
            inputString = string.Join(",", values);
            int expected = 0;
            values.ForEach(val => expected += val);

            Act();
            result.Should().Be(expected);
        }

        private List<int> GetValues()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return Enumerable.Range(0, rand.Next(0, 100)).Select(val => rand.Next(0, 100)).ToList();
        }

        [Fact]
        public void Test_random()
        {
            var ar1 = GetValues();
            var ar2 = GetValues();
            ar1.Should().NotBeEquivalentTo(ar2);
        }

        [Fact]
        public void Add_NewLineSeparator_ReturnsSum()
        {
            inputString = "2\n3,4";
            Act();
            result.Should().Be(9);
        }

        [Fact]
        public void Add_OneCharDelimiter_ReturnsSum()
        {
            inputString = "//;\n2;3;4";
            Act();
            result.Should().Be(9);
        }

        [Fact]
        public void Add_NegativeValues_Throws()
        {
            inputString = "2,-3,4,-5";
            calculator.Invoking(calc => calc.Add(inputString)).ShouldThrow<Exception>(
                "negatives are not allowed: -3, -5");
        }

        [Fact]
        public void Add_ValuesGreaterThen1000_Ignored()
        {
            inputString = "2,3,1002,5,1005";
            Act();
            result.Should().Be(10);
        }
        
    }

    public class Calculator
    {
        public int Add(string number)
        {
            var separators = new[] {",", "\n"};
            if (number.StartsWith("//"))
            {
                separators = new[] {number.Substring(2, 1)};
                number = Regex.Replace(number, "//(.*?)\n", string.Empty);
            }
            var values = number.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var negatives = values.ToList().Where(val => val.StartsWith("-"));
            if (negatives.Any())
                throw new Exception(String.Format("negatives are not allowed: {0}", String.Join(" ,", negatives)));

            return values.Select(Int32.Parse).Where(v => v <= 1000).Sum();
        }
    }
}