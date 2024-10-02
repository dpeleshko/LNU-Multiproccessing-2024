using System;
using System.Threading;

class Program
{
    static int x = 3;
    static int y = 5;
    static int z = 4;

    static SemaphoreSlim sem_T3 = new SemaphoreSlim(0, 1);
    static SemaphoreSlim sem_T5_T1 = new SemaphoreSlim(0, 1);
    static SemaphoreSlim sem_T5_T2 = new SemaphoreSlim(0, 1);
    static SemaphoreSlim sem_T5_T4 = new SemaphoreSlim(0, 1);

    static void Main(string[] args)
    {

        Thread t1 = new Thread(() =>
        {
            y = y + 2;
            Console.WriteLine($"T1 done: y = {y}");
            sem_T3.Release();
            sem_T5_T1.Release();
        });

        Thread t2 = new Thread(() =>
        {
            x = x * 5;
            Console.WriteLine($"T2 done: x = {x}");
            sem_T5_T2.Release();
        });

        Thread t3 = new Thread(() =>
        {
            sem_T3.Wait();
            y = y * 2;
            Console.WriteLine($"T3 done: y = {y}");
        });

        Thread t4 = new Thread(() =>
        {
            z = z - 3;
            Console.WriteLine($"T4 done: z = {z}");
            sem_T5_T4.Release();
        });


        Thread t5 = new Thread(() =>
        {
            sem_T5_T1.Wait();
            sem_T5_T2.Wait();
            sem_T5_T4.Wait();
            x = x + y + z;
            Console.WriteLine($"T5 done: x = {x}");
        });

        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();

        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();
        t5.Join();
    }
}
