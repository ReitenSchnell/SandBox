using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Maths;
using MicrosoftResearch.Infer.Models;
using Ninject;

namespace InferNet
{
    public class LearningGaussian
    {
        private double[] data;
        private Variable<double> mean;
        private Variable<double> precision;

        private static InferenceEngine InferenceEngine
        {
            get { return DependencyWrapper.Kernel.Get<InferenceEngine>(); }
        }

        public object LearnSlowly()
        {
            SetupData();
            SetupStatistics();

            for (var i = 0; i < data.Length; i++)
            {
                var x = Variable.GaussianFromMeanAndPrecision(mean, precision).Named("x" + i);
                x.ObservedValue = data[i];
            }
            return InferenceEngine.Infer(mean);
        }

        public object LearnFast()
        {
            SetupData();
            SetupStatistics();

            var dataRange = new Range(data.Length);
            var x = Variable.Array<double>(dataRange);
            x[dataRange] = Variable.GaussianFromMeanAndPrecision(mean, precision).ForEach(dataRange);
            x.ObservedValue = data;

            return InferenceEngine.Infer(mean);
        }

        private void SetupStatistics()
        {
            mean = Variable.GaussianFromMeanAndVariance(0, 100);
            precision = Variable.GammaFromShapeAndScale(1, 1);
        }

        private void SetupData()
        {
            data = new double[100];
            for (var i = 0; i < data.Length; i++) 
                data[i] = Rand.Normal(0, 1);
        }
    }
}