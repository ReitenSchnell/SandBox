using System;
using System.Linq;
using System.Threading;

namespace ThreadsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(DoY);
            thread.Start();
            WriteSymbol('x');
            Console.Read();
        }

        private static void DoY()
        {
            WriteSymbol('y');
        }

        private static void WriteSymbol(char symbol)
        {
            Enumerable.Range(0, 1000).ToList().ForEach(i => Console.Write(symbol));
        }
    }
}
