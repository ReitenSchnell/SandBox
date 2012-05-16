using MicrosoftResearch.Infer.Models;
using MicrosoftResearch.Infer;
using Ninject;

namespace InferNet
{
    public class TwoCoins
    {
        private static InferenceEngine InferenceEngine 
        { 
            get { return DependencyWrapper.Kernel.Get<InferenceEngine>(); }
        }
        
        public object ForwardProbability(double firstDistribution, double secondDistribution)
        {
            var firstCoin = Variable.Bernoulli(firstDistribution);
            var secondCoin = Variable.Bernoulli(secondDistribution);
            var bothHeads = firstCoin & secondCoin;
            return InferenceEngine.Infer(bothHeads);
        }

        public object BackwardProbability(double firstDistribution, double secondDistribution, bool result)
        {
            var firstCoin = Variable.Bernoulli(firstDistribution);
            var secondCoin = Variable.Bernoulli(secondDistribution);
            var bothHeads = firstCoin & secondCoin;
            bothHeads.ObservedValue = result;
            return InferenceEngine.Infer(firstCoin);
        }
    }
}
