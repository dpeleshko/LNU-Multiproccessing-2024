using System;
using System.Diagnostics;
using System.Threading;

namespace ParallelComputation
{

    class Program
    {
        static EventWaitHandle wh1 = new AutoResetEvent(false),
                               wh2 = new AutoResetEvent(false),
                               wh3 = new AutoResetEvent(false);

        static int x1, x2, x3, x4, x5, x6;
        static int resultPart1, resultPart2;
        static int F;

        static void Main(string[] args)
        {
            Thread t1 = new Thread(ComputePart1);
            Thread t2 = new Thread(ComputePart2);
            Thread t3 = new Thread(MultiplyResults);
            Thread t4 = new Thread(ComputeFinalResult);

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t4.Join();

            Console.WriteLine($"F = {F}");
        }

        static void ComputePart1()
        {
            x1 = 1;
            x2 = 2;
            resultPart1 = x1 + x2;
            wh1.Set();
        }

        static void ComputePart2()
        {
            x3 = 3;
            x4 = 4;
            resultPart2 = x3 + x4;
            wh2.Set();
        }

        static void MultiplyResults()
        {
            wh1.WaitOne();
            wh2.WaitOne();
            resultPart1 *= resultPart2;
            wh3.Set();
        }

        static void ComputeFinalResult()
        {
            x5 = 5;
            x6 = 6;
            wh3.WaitOne();
            F = resultPart1 + x5 + x6;
        }
    }

}