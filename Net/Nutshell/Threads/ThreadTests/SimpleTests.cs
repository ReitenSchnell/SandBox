using System;
using System.Threading;
using Xunit;

namespace ThreadTests
{
    public class SimpleTests
    {
        [Fact]
        public void test_shared_state()
        {
            var done = false;
            ThreadStart action = () =>
                                     {
                                         if (!done)
                                         {
                                             done = true;
                                             Console.Out.WriteLine("Done");
                                         }
                                     };
            new Thread(action).Start();
            action();
        }

        [Fact]
        public void test_shared_state_with_lock()
        {
            var done = false;
            var locker = new object();
            ThreadStart action = () =>
            {
                lock (locker)
                {
                    if (done) return;
                    done = true;
                    Console.Out.WriteLine("Done");
                }
            };
            new Thread(action).Start();
            action();
        }
    }
}
