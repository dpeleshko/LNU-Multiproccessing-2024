
using System;
using System.Threading;

namespace lab3
{
    class Program
    {
        
        static EventWaitHandle wh1 = new AutoResetEvent(false),
            wh2 = new AutoResetEvent(false),
            wh3 = new AutoResetEvent(false),
            wh4 = new AutoResetEvent(false);

       
        static int x1, x2, x3, x4, x5, x6;
        static int sum1, sum2, product1, result;

        static void Main(string[] args)
        {
            
            var T0 = new Thread(InitializeAndAddX1X2);
            var T1 = new Thread(InitializeAndAddX3X4);
            var T2 = new Thread(MultiplySums);
            var T3 = new Thread(MultiplyWithX5);
            var T4 = new Thread(AddX6);

         
            T0.Start();
            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();

          
            T4.Join();

            Console.WriteLine("F = {0}", result);
        }

        
        static void InitializeAndAddX1X2()
        {
            x1 = 1;
            x2 = 2;
            sum1 = x1 + x2; 
            wh1.Set();       
        }

       
        static void InitializeAndAddX3X4()
        {
            x3 = 3;
            x4 = 4;
            sum2 = x3 + x4; 
            wh2.Set();      
        }

       
        static void MultiplySums()
        {
            wh1.WaitOne();    
            wh2.WaitOne();    
            product1 = sum1 * sum2; 
            wh3.Set();      
        }

      
        static void MultiplyWithX5()
        {
            x5 = 5;
            wh3.WaitOne();   
            product1 *= x5;  
            wh4.Set();       
        }

       
        static void AddX6()
        {
            x6 = 6;
            wh4.WaitOne();    
            result = product1 + x6;  
        }
    }
}
