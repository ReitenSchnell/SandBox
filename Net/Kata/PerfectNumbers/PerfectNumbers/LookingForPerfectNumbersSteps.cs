using System;
using TechTalk.SpecFlow;

namespace PerfectNumbers
{
    [Binding]
    public class LookingForPerfectNumbersSteps
    {
        [When]
        public void When_Asked_if_the_number_is_not_perfect()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
