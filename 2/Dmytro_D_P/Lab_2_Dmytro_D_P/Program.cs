namespace Lab_2_D_D
{
    internal class Program
    {
        static double x=9, y=5, z=4;
        static Semaphore S1 = new Semaphore(0, 1);
        static Semaphore S2 = new Semaphore(0, 1);
        static Semaphore S3 = new Semaphore(0, 1);
        static Semaphore S4 = new Semaphore(0, 1);
        static Semaphore S5 = new Semaphore(0, 1);
        static void Main(string[] args)
        {
            Thread T1 = new Thread(() => 
            {
                x *= 5;
                S1.Release();
            });
            Thread T2 = new Thread(() =>
            {
                y+=2;
                S2.Release();
            });
            Thread T3 = new Thread(() => 
            {
                S1.WaitOne();
                x += 2;
                S3.Release();
            });
            Thread T4 = new Thread(() =>
            {
                S2.WaitOne();
                S3.WaitOne();
                y += -3;
                S4.Release();
            });
            Thread T5 = new Thread(() => 
            {
                S4.WaitOne();
                y = x * y;
            });
            Console.WriteLine("x = {0}; y = {1}; z = {2};", x, y, z);
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
            Console.WriteLine("x = {0}; y = {1}; z = {2};", x, y, z);
            Console.ReadKey();
        }
    }
}