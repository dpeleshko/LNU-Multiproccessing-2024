namespace Lab2
{
    internal class Program
    {
        static double x = 2, y = 5, z = 4;
        static SemaphoreSlim semaphoreT2 = new(0);
        static SemaphoreSlim semaphoreT3 = new(0);
        static SemaphoreSlim semaphoreT4 = new(0);
        static SemaphoreSlim semaphoreT5 = new(0);

        static void ThreadT1()
        {
            x = x / 4;
            semaphoreT2.Release();
        }

        static void ThreadT2()
        {
            semaphoreT2.Wait();
            y = y - 3;
            semaphoreT3.Release();
            semaphoreT4.Release();
        }

        static void ThreadT3()
        {
            semaphoreT3.Wait();
            x = x + 8;
            semaphoreT5.Release();
        }

        static void ThreadT4()
        {
            semaphoreT4.Wait();
            y = y * 5;
            semaphoreT5.Release();
        }

        static void ThreadT5()
        {
            semaphoreT5.Wait();
            z = x + y;
            Console.WriteLine("Результат: z = " + z);
        }


        static void Main(string[] args)
        {
            Thread t1 = new(ThreadT1);
            Thread t2 = new(ThreadT2);
            Thread t3 = new(ThreadT3);
            Thread t4 = new(ThreadT4);
            Thread t5 = new(ThreadT5);

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
}
