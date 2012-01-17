using System.Collections.Generic;
using InferNet;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using MicrosoftResearch.Infer.Maths;
using MicrosoftResearch.Infer.Models;
using Xunit;

namespace InferNetTests
{
    public class BayesPointMachineTests
    {
        [Fact]
        public void CheckTrain()
        {
            double[] incomes = { 63, 16, 28, 55, 22, 20 };
            double[] ages = { 38, 23, 40, 27, 18, 40 };
            bool[] willBuy = { true, false, true, true, false, false };
            double[] incomesTest = { 58, 18, 22 };
            double[] agesTest = { 36, 24, 37 };
            var machine = new BayesPointMachine();
            var model = machine.Train(incomes, ages, willBuy);
            var result= machine.Test(incomesTest, agesTest, model);
            var r = result[0];
            var d = r.GetProbTrue();
        }

        [Fact]
        public void Test()
        {
            bool[] ydata = { true, false, true, true, false, false };
            var y = Variable.Observed(ydata);
            var j = y.Range;
            
            double[] incomes = { 63, 16, 28, 55, 22, 20 };
            double[] ages = { 38, 23, 40, 27, 18, 40 };
            var xdata = new Vector[(incomes as IList<double>).Count];
            for (var i = 0; i < xdata.Length; i++)
            {
                xdata[i] = Vector.FromList(new List<double> { incomes[i], ages[i], 1 });
            }
            var res = Variable.Observed(xdata, j);

            var w = Variable.Random(new VectorGaussian(Vector.Zero(3), PositiveDefiniteMatrix.Identity(3)));
            var gaussianFromMeanAndVariance = Variable.GaussianFromMeanAndVariance(Variable.InnerProduct(w, res[j]), 0.5);
            y[j] = gaussianFromMeanAndVariance > 0;
            var inf = (VectorGaussian)(new InferenceEngine()).Infer(w);
        }
    }
}