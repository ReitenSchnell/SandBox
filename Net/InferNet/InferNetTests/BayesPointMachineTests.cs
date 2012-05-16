using InferNet;
using Xunit;
using System.Linq;

namespace InferNetTests
{
    public class BayesPointMachineTests
    {
        [Fact]
        public void CheckTrain()
        {
            //model data
            double[] incomes = { 63, 16, 28, 55, 22, 20 };
            double[] ages = { 38, 23, 40, 27, 18, 40 };
            bool[] willBuy = { true, false, true, true, false, false };

            //test data
            double[] incomesTest = { 58, 18, 22 };
            double[] agesTest = { 36, 24, 37 };
            bool[] yTest = { true, false, false };

            //train and infer
            var machine = new BayesPointMachine();
            var model = machine.Train(incomes, ages, willBuy);
            var result= machine.Test(incomesTest, agesTest, model);
            var resultInBool = result.Select(bernoulli => bernoulli.GetProbTrue() > 0.5 ? true : false).ToList();

            //check accuracy
            var accuracy = 0;
            for (var i = 0; i < resultInBool.Count(); i++)
            {
                accuracy += resultInBool[i] == yTest[i] ? 1:0;
            }
            Assert.Equal(100, accuracy*100/resultInBool.Count);
        }
    }
}