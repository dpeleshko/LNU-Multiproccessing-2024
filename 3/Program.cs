namespace lab3
{
    internal class Program
    {

        //(x1+ x2)(x3 + x4 + x5 + x6)

        /* static EventWaitHandle wh1 = new AutoResetEvent(false),
                                wh2 = new AutoResetEvent(false);

         static int x1, x2, x3, x4, x5, x6;
         static int A, B;

         static void Main(string[] args)
         {
             Thread t0 = new Thread(F0);
             Thread t1 = new Thread(F1);
             Thread t2 = new Thread(F2);

             t0.Start();
             t1.Start();
             t2.Start();
             t2.Join();
         }

         static void F0()
         {
             x1= 1;
             x2= 2;
             A = x1 + x2;
             wh1.Set();
         }
         static void F1()
         {
             x3 = 3;
             x4 = 4;
             x5 = 5;
             x6 = 6;
             B= x3 + x4 + x5 + x6;
             wh2.Set();
         }
         static void F2()
         {
             wh1.WaitOne();
             wh2.WaitOne();
             Console.WriteLine("F = {0}", A * B);
         }*/

        // (x1 x2 + x3)(x4 + x5) x6

        static EventWaitHandle wh1 = new AutoResetEvent(false),
                               wh2 = new AutoResetEvent(false),
                               wh3 = new AutoResetEvent(false);

        static int x1, x2, x3, x4, x5, x6;
        static int A, B;

        static void Main(string[] args)
        {
            Thread t0 = new Thread(F0);
            Thread t1 = new Thread(F1);
            Thread t2 = new Thread(F2);
            Thread t3 = new Thread(F3);

            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t3.Join();
        }

        static void F0()
        {
            x1 = 1;
            x2 = 2;
            x3 = 3;
            A = (x1 * x2) + x3;
            wh1.Set();
        }
        static void F1()
        {
            x4 = 4;
            x5 = 5;
            B = x4 + x5;
            wh2.Set();
        }
        static void F2()
        {
            x6 = 6;
            wh2.WaitOne();
            B *= x6;
            wh3.Set();
        }
        static void F3()
        {
            wh1.WaitOne();
            wh3.WaitOne();
            Console.WriteLine("F = {0}", A * B);
        }
    }
}