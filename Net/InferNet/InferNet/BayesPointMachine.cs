using System.Collections.Generic;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using MicrosoftResearch.Infer.Maths;
using MicrosoftResearch.Infer.Models;
using Ninject;

namespace InferNet
{
    public class BayesPointMachine
    {
        private static InferenceEngine InferenceEngine
        {
            get { return DependencyWrapper.Kernel.Get<InferenceEngine>(); }
        }

        const double noise = 0.1;

        public VectorGaussian Train(double[] incomes, double[] ages, bool[] ydata)
        {
            var y = Variable.Observed(ydata);
            var j = y.Range;
            var x = SetupFeatures(incomes, ages, j);
            var w = Variable.Random(new VectorGaussian(Vector.Zero(3), PositiveDefiniteMatrix.Identity(3)));
            y[j] = Variable.GaussianFromMeanAndVariance(Variable.InnerProduct(w, x[j]), noise) > 0;
            return (VectorGaussian)InferenceEngine.Infer(w);
        }

        public DistributionArray<Bernoulli, bool> Test(double[] incomes, double[] ages, VectorGaussian weights)
        {
            var ytest = Variable.Array<bool>(new Range(ages.Length));
            var j = ytest.Range;
            var xtest = SetupFeatures(incomes, ages, j);
            ytest[j] = Variable.GaussianFromMeanAndVariance(Variable.InnerProduct(Variable.Random(weights), xtest[j]), noise) > 0;
            return (DistributionArray<Bernoulli, bool>)InferenceEngine.Infer(ytest);
        }

        private static VariableArray<Vector> SetupFeatures(IList<double> incomes, IList<double> ages, Range j)
        {
            var xdata = new Vector[incomes.Count];
            for (var i = 0; i < xdata.Length; i++)
            {
                xdata[i] = Vector.FromList(new List<double> { incomes[i], ages[i], 1 });
            }
            return Variable.Observed(xdata, j);
        }
    }
}