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
    }

    public class Calculator
    {
        public int Add(string number)
        {
            return 999;
        }
    }
}