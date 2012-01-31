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

        [Fact]
        public void Add_OneNumberIsNegative_Throws()
        {
            calculator.Invoking(calc => calc.Add("-3,4,-55")).ShouldThrow<Exception>().WithMessage("negatives not allowed: -3,-55");
        }

        [Fact]
        public void Add_NumbersBiggerThen1000_Ignores()
        {
            var result = calculator.Add("3,1001,2");
            result.Should().Be(5);
        }


        [Fact]
        public void Add_OneNumberIs1000_NotIgnores()
        {
            var result = calculator.Add("3,1000,2");
            result.Should().Be(1005);
        }

        [Fact]
        public void Add_TwoCharDelimiter_ReturnsSum()
        {
            var result = calculator.Add("//***\n1***2***3");
            result.Should().Be(6);
        }
    }

    public class Calculator
    {
        public int Add(string number)
        {
            if (string.IsNullOrEmpty(number))
                return 0;
            
            string[] values;
            const string separatorStart = "//";
            const string newString = "\n";
            if (number.StartsWith(separatorStart))
            {
                var newSeparator = number.Substring(separatorStart.Length, number.IndexOf(newString) - separatorStart.Length);
                number = number.Substring(number.IndexOf(newString)+newString.Length);
                values = number.Split(new[] {newSeparator}, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                var defaultSeparators = new[] { ",", newString };
                values = (number.Split(defaultSeparators, StringSplitOptions.RemoveEmptyEntries));
            }

            var negatives = values.Where(val => val.StartsWith("-")).ToArray();
            if (negatives.Any())
                throw new Exception(String.Format("negatives not allowed: {0}", string.Join(",", negatives)));
            

            return values.Select(Int32.Parse).Where(val => val<=1000).Sum();
        }
    }
}