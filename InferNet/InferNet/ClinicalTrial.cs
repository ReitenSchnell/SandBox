using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using MicrosoftResearch.Infer.Models;
using Ninject;

namespace InferNet
{
    public class TrialResults
    {
        public Bernoulli IsEffective { get; set; }
        public double ProbControl { get; set; }
        public double ProbTreated { get; set; }
    }

    public class ClinicalTrial
    {
        private Variable<bool> isEffective;
        private Variable<double> probControl;
        private Variable<double> probTreated;

        private static InferenceEngine InferenceEngine
        {
            get { return DependencyWrapper.Kernel.Get<InferenceEngine>(); }
        }

        private void CreateModel(bool[] treatedData, bool[] controlData)
        {
            var treatedGroup = Variable.Observed(treatedData);
            var controlGroup = Variable.Observed(controlData);
            var i = treatedGroup.Range;
            var j = controlGroup.Range;
            isEffective = Variable.Bernoulli(0.5);
            using (Variable.If(isEffective))
            {
                probControl = Variable.Beta(1, 1);
                controlGroup[j] = Variable.Bernoulli(probControl).ForEach(j);
                probTreated = Variable.Beta(1, 1);
                treatedGroup[i] = Variable.Bernoulli(probTreated).ForEach(i);
            }
            using (Variable.IfNot(isEffective))
            {
                var probAll = Variable.Beta(1, 1);
                controlGroup[j] = Variable.Bernoulli(probAll).ForEach(j);
                treatedGroup[i] = Variable.Bernoulli(probAll).ForEach(i);
            }
        }

        public bool IsTreatmentEffective(bool[] treatedData, bool[] controlData)
        {
            CreateModel(treatedData, controlData);
            return ((Bernoulli) InferenceEngine.Infer(isEffective)).GetProbTrue() > 0.5;
        }

        public TrialResults GetResults(bool[] treatedData, bool[] controlData)
        {
            CreateModel(treatedData, controlData);
            return new TrialResults
                       {
                           IsEffective = (Bernoulli)InferenceEngine.Infer(isEffective),
                           ProbControl = InferenceEngine.Infer<Beta>(probControl).GetMean(),
                           ProbTreated = InferenceEngine.Infer<Beta>(probTreated).GetMean()
                       };
        }
    }
}