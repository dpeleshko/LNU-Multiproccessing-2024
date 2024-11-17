using System;
using System.Threading;

class Program
{
    static EventWaitHandle wh1 = new AutoResetEvent(false);
    static EventWaitHandle wh2 = new AutoResetEvent(false);
    static EventWaitHandle wh3 = new AutoResetEvent(false);
    static EventWaitHandle wh4 = new AutoResetEvent(false);

    static int x1, x2, x3, x4, x5, x6;
    static int sum1, sum2, mult1, mult2, result;

    static void Main()
    {
        Thread t1 = new Thread(CalcSum1);
        Thread t2 = new Thread(CalcSum2);
        Thread t3 = new Thread(CalcMult1);
        Thread t4 = new Thread(CalcMult2);
        Thread t5 = new Thread(CalcResult);

        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();

        t5.Join();

        Console.WriteLine($"Результат: F = {result}");
        Console.ReadKey();
    }

    static void CalcSum1()
    {
        x1 = 1;
        x2 = 2;
        sum1 = x1 + x2;
        wh1.Set();
    }

    static void CalcSum2()
    {
        x3 = 3;
        x4 = 4;
        sum2 = x3 + x4;
        wh2.Set();
    }

    static void CalcMult1()
    {
        wh1.WaitOne();
        wh2.WaitOne();
        mult1 = sum1 * sum2;
        wh3.Set();
    }

    static void CalcMult2()
    {
        x5 = 5;
        x6 = 6;
        mult2 = x5 * x6;
        wh4.Set();
    }

    static void CalcResult()
    {
        wh3.WaitOne();
        wh4.WaitOne();
        result = mult1 + mult2;
    }
}