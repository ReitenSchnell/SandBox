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
        public void GetTrialResults_NotEffectiveTrial_CheckResult()
        {
            var result = clinicalTrial.GetResults(new[] { false, false, true, false, false }, new[] { false, false, false, true, false });
            Assert.Equal(result.ProbTreated, result.ProbControl);
        }

        [Fact]
        public void GetTrialResults_EffectiveTrial_CheckResult()
        {
            var result = clinicalTrial.GetResults(new[] { true, false, true, true, true }, new[] { false, false, true, false, false });
            Assert.True(result.ProbTreated > result.ProbControl);
        }

    }
}