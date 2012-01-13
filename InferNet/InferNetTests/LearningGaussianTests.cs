using System.Diagnostics;
using InferNet;
using Xunit;

namespace InferNetTests
{
    public class LearningGaussianTests
    {
        private readonly LearningGaussian learningGaussian = new LearningGaussian();

        [Fact]
        public void CompareTime()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            learningGaussian.LearnSlowly();
            stopwatch.Stop();
            var slow = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            learningGaussian.LearnFast();
            stopwatch.Stop();
            var fast = stopwatch.ElapsedMilliseconds;
            Assert.True(fast < slow);
        }
    }
}