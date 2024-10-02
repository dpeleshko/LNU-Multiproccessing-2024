using System;
using System.Threading;

class Program
{
    static double x = 16, y = 10;  
    static Semaphore S1 = new Semaphore(0, 1); 
    static Semaphore S2 = new Semaphore(0, 1);  
    static Semaphore S3 = new Semaphore(0, 1);  
    static Semaphore S4 = new Semaphore(0, 1);  

    static void Main(string[] args)
    {
        Console.WriteLine("Початкові значення: x = {0}, y = {1}", x, y);

        Thread T1 = new Thread(() =>
        {
            x /= 4;
            S1.Release();  
        });

        
        Thread T2 = new Thread(() =>
        {
            y -= 3;
            S2.Release(); 
        });

        
        Thread T3 = new Thread(() =>
        {
            S1.WaitOne();  
            x += 8;
            S3.Release();  
        });

        
        Thread T4 = new Thread(() =>
        {
            S2.WaitOne();  
            y *= 5;
            S4.Release();  
        });

        
        Thread T5 = new Thread(() =>
        {
            S3.WaitOne(); 
            S4.WaitOne();  
            y = x + y;
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

        
        Console.WriteLine("Підсумкові значення: x = {0}, y = {1}", x, y);
    }
}
