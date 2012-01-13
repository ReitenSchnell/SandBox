using Ninject;

namespace InferNet
{
    public class DependencyWrapper
    {
        public static readonly IKernel Kernel = new StandardKernel();
    }
}