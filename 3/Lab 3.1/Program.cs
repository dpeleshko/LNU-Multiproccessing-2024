namespace Lab31
{
    internal class Program
    {

        static EventWaitHandle wh1 = new AutoResetEvent(false),
            wh2 = new AutoResetEvent(false),
            wh3 = new AutoResetEvent(false),
            wh4 = new AutoResetEvent(false);

        static int x1, x2, x3, x4, x5, x6;
        static int multiple1, sum1, sum2, sum3, result;
        static void Main(string[] args)
        {

            var thread1 = new Thread(Multiple1);
            var thread2 = new Thread(Sum1);
            var thread3 = new Thread(Sum2);
            var thread4 = new Thread(Sum3);
            var thread5 = new Thread(Sum4);


            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

            thread5.Join();

            Console.WriteLine(result);



            static void Multiple1()
            {
                x2 = 2;
                x3 = 3;
                multiple1 = x2 * x3;
                wh1.Set();
            }

            static void Sum1()
            {
                wh1.WaitOne();
                x1 = 1;
                sum1 = x1 + multiple1;
                wh2.Set();
            }

            static void Sum2()
            {
                wh2.WaitOne();
                x4 = 4;
                sum2 = x4 + sum1;
                wh3.Set();
            }

            static void Sum3()
            {
                wh3.WaitOne();
                x5 = 5;
                sum3 = x5 + sum2;
                wh4.Set();
            }

            static void Sum4()
            {
                wh4.WaitOne();
                x6 = 6;
                result = x6 + sum3;
            }

        }
    }
}

