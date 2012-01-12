using MicrosoftResearch.Infer.Models;
using MicrosoftResearch.Infer;


namespace InferNetExamples
{
    public class TwoCoins
    {
        public object ForwardProbability(double firstDistribution, double secondDistribution)
        {
            var firstCoin = Variable.Bernoulli(firstDistribution);
            var secondCoin = Variable.Bernoulli(secondDistribution);
            var bothHeads = firstCoin & secondCoin;
            var engine = new InferenceEngine();
            return engine.Infer(bothHeads);
        }

        public object BackwardProbability(double firstDistribution, double secondDistribution, bool result)
        {
            var firstCoin = Variable.Bernoulli(firstDistribution);
            var secondCoin = Variable.Bernoulli(secondDistribution);
            var bothHeads = firstCoin & secondCoin;
            bothHeads.ObservedValue = result;
            var engine = new InferenceEngine();
            return engine.Infer(firstCoin);
        }
    }
}
