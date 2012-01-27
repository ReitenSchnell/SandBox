using InferNet;
using Xunit;

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
            var result = clinicalTrial.IsTreatmentEffective(new[] { false, false, true, false, false }, new[] { true, false, true, true, false });
            Assert.False(result);
        }

    }
}