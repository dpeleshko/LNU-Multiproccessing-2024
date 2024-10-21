using System;
using System.Threading;
namespace Lab3_1
{

    class Program
    {

        static EventWaitHandle wh1 = new AutoResetEvent(false),
                               wh2 = new AutoResetEvent(false),
                               wh3 = new AutoResetEvent(false);
        static private int x1, x2, x3, x4, x5, x6;
        static int A, B, C;
        static void Main()
        {
            var T0 = new Thread(F0);
            var T1 = new Thread(F1);
            var T2 = new Thread(F2);
            var T3 = new Thread(F3);

            T0.Start();
            T1.Start();
            T2.Start();
            T3.Start();

            // Дочекаємось завершення всіх потоків
            T0.Join();
            T1.Join();
            T2.Join();
            T3.Join();

        }
        static void F0() 
        {
            x1 = 1;
            x2 = 2;
            A = x1 + x2;
            wh1.Set();
        }
        static void F1() 
        {
            x3 = 3;
            x4 = 4;  
            B= x3 + x4;
            wh2.Set();
        }
        static void F2() 
        {
            x5 = 5;
            x6 = 6;
            C = x5 + x6;
            wh3.Set();
        }
        static void F3() 
        {
            wh1.WaitOne();
            wh2.WaitOne();  
            wh3.WaitOne();           
            Console.WriteLine($"F ={A * B * C}");
        }
    }
}
