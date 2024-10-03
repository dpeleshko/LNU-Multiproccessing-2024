using System;
using System.Threading;

class Program
{
  
    static int x = 8;
    static int y = 5;
    static int z = 4;

    
    static SemaphoreSlim semT1T2 = new SemaphoreSlim(0, 2);  
    static SemaphoreSlim semT3 = new SemaphoreSlim(0, 1);    
    static SemaphoreSlim semT4 = new SemaphoreSlim(0, 1);   

    static void T1()
    {
        x += 3;
      
        semT1T2.Release();  
    }

    static void T2()
    {
        z += 2;
     
        semT1T2.Release();  
    }

    static void T3()
    {
        y *= 2;
       
        semT3.Release();  
    }

    static void T4()
    {
        semT1T2.Wait();  
        semT1T2.Wait();  
        x = z * x;
      
        semT4.Release();  
    }

    static void T5()
    {
        semT3.Wait();  
        semT4.Wait();  
        y = x * y;
      
    }

    static void Main()
    {
      
        Thread thread1 = new Thread(T1);
        Thread thread2 = new Thread(T2);
        Thread thread3 = new Thread(T3);
        Thread thread4 = new Thread(T4);
        Thread thread5 = new Thread(T5);

      
        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
        thread5.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();
        thread4.Join();
        thread5.Join();

        Console.WriteLine($"Фінальні значення: x = {x}, y = {y}, z = {z}");
    }
}
