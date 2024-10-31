using System.Diagnostics;
using System.Threading;

namespace Lab3_1
{

    class _1_task
    {
        static EventWaitHandle wh1 = new AutoResetEvent(false),
          wh2 = new AutoResetEvent(false),
          wh3 = new AutoResetEvent(false);

        static private int x1, x2, x3, x4, x5, x6;
        static int A, B, C, D, result;

        static void Main(string[] args)
        {
            Console.WriteLine("Вираз: x1x2 + x3x4 + x5 + x6");
            Stopwatch stopwatch = Stopwatch.StartNew();
            var T0 = new Thread(Func0);
            var T1 = new Thread(Func1);
            var T2 = new Thread(Func2);
            var T3 = new Thread(Func3);
            var T4 = new Thread(Func4);
            T0.Start();
            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T4.Join();

            stopwatch.Stop();
            Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");
        }

        static void Func0()
        {
            x1 = 1;
            x2 = 2;
            A = x1 * x2;
            wh1.Set();
        }

        static void Func1()
        {
            x3 = 3;
            x4 = 4;
            B = x3 * x4;
            wh2.Set();
        }
        
        static void Func2()
        {
            x5 = 5;
            x6 = 6;
            C = x5 + x6;
            wh3.Set();
        }

        static void Func3()
        {
            wh1.WaitOne();
            wh2.WaitOne();
            D = A + B;
            wh3.Set();
        }

        static void Func4()
        {
            wh3.WaitOne();
            result = D + C;
            Console.WriteLine("Result = {0}", result);
        }
    }
}
