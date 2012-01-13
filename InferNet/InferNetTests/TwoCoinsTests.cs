using InferNet;
using Xunit;

namespace InferNetExamplesTests
{
    public class TwoCoinsTests
    {
        private readonly TwoCoins twoCoins = new TwoCoins();
        
        [Fact]
        public void ForwardProbability_CheckResult()
        {
            var result = twoCoins.ForwardProbability(0.5, 0.5);
            Assert.Equal("Bernoulli(0,25)", result.ToString());
        }

        [Fact]
        public void BackwardProbabilityObserveFalse_CheckResult()
        {
            var result = twoCoins.BackwardProbability(0.5, 0.5, false);
            Assert.Equal("Bernoulli(0,3333)", result.ToString());
        }

        [Fact]
        public void BackwardProbabilityObserveTrue_CheckResult()
        {
            var result = twoCoins.BackwardProbability(0.5, 0.5, true);
            Assert.Equal("Bernoulli(1)", result.ToString());
        }
    }
}
