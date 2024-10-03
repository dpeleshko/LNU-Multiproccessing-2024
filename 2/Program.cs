using System;
using System.Threading;

namespace ThreadSync
{
    class Program
    {
        static int x = 7, y = 5, z = 4; 
        static Semaphore S1 = new Semaphore(0, 1);
        static Semaphore S2 = new Semaphore(0, 1); 
        static Semaphore S3 = new Semaphore(0, 1); 
        static Semaphore S4 = new Semaphore(0, 1); 

        static void Main(string[] args)
        {
            Thread T1 = new Thread(() =>
            {
                y = y + 2;
                Console.WriteLine("T1: y = y + 2 -> y = " + y);
                S1.Release(); 
            });

            Thread T2 = new Thread(() =>
            {
                x = x * 5;
                Console.WriteLine("T2: x = x * 5 -> x = " + x);
                S2.Release(); 
            });

            Thread T3 = new Thread(() =>
            {
                z = z / 2;
                Console.WriteLine("T3: z = z / 2 -> z = " + z);
                S3.Release(); 
            });

            Thread T4 = new Thread(() =>
            {
                S2.WaitOne(); 
                S3.WaitOne(); 
                z = x - z;
                Console.WriteLine("T4: z = x - z -> z = " + z);
                S4.Release();
            });

            Thread T5 = new Thread(() =>
            {
                S1.WaitOne(); 
                S4.WaitOne(); 
                y = x * z;
                Console.WriteLine("T5: y = x * z -> y = " + y);
            });


            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T5.Start();

            T1.Join();
            T2.Join();
            T3.Join();
            T4.Join();
            T5.Join();

            Console.WriteLine("Final values: x = {0}, y = {1}, z = {2}", x, y, z);
        }
    }
}
