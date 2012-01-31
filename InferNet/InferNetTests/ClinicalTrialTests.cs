using InferNet;
using Xunit;
using FluentAssertions;

namespace InferNetTests
{
    public class ClinicalTrialTests
    {
        private readonly ClinicalTrial clinicalTrial = new ClinicalTrial();

        [Fact]
        public void IsTreatmentEffective_ReturnTrue()
        {
            var result = clinicalTrial.IsTreatmentEffective(new[] { true, false, true, true, true }, new[] { false, false, true, false, false });
            Assert.True(result);
        }

        [Fact]
        public void IsTreatmentEffective_ReturnFalse()
        {
            var result = clinicalTrial.IsTreatmentEffective(new[] { false, false, true, false, false }, new[] { false, false, false, true, false });
            Assert.False(result);
        }

        [Fact]
        public void GetTrialResults_CheckResult()
        {
            var result = clinicalTrial.GetResults(new[] { false, false, true, false, false }, new[] { false, false, false, true, false });
            var probControl = result.ProbControl.GetProbTrue();
            var probTreated = result.ProbTreated.GetProbTrue();
            
        }

    }
}