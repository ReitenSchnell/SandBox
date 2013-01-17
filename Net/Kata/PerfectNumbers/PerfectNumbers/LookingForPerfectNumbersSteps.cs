using System;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace PerfectNumbers
{
    [Binding]
    public class LookingForPerfectNumbersSteps
    {
        private int number;
        private bool result;

        [Given(@"Nonperfect number")]
        public void GivenNonperfectNumber()
        {
            number = 490;
        }
        
        [Given(@"Perfect number")]
        public void GivenPerfectNumber()
        {
            number = 496;
        }
        
        [When(@"Asked if the number is perfect")]
        public void WhenAskedIfTheNumberIsPerfect()
        {
            result = new PerfectNumbers().IsPerfectNumber(number);
        }
        
        [Then(@"the result should be False")]
        public void ThenTheResultShouldBeFalse()
        {
            result.Should().BeFalse();
        }
        
        [Then(@"the result should be True")]
        public void ThenTheResultShouldBeTrue()
        {
            result.Should().BeTrue();
        }
    }
}
