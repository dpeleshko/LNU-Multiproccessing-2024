namespace Lab2
{
    class Task5
    {
        private static int x = 3;
        private static int y = 3;

        private static Semaphore s1 = new Semaphore(0, 1);
        private static Semaphore s2 = new Semaphore(0, 1);
        private static Semaphore s3 = new Semaphore(0, 1);
        private static Semaphore s4 = new Semaphore(0, 1);

        private static void T1()
        {
            x *= 5;
            s1.Release();
        }
        private static void T2()
        {
            y += 2;
            s2.Release();
        }
        private static void T3()
        {
            s1.WaitOne();
            s2.WaitOne();

            y -= 3;
            s3.Release();   
        }
        private static void T4()
        {
            s3.WaitOne();   
            x += 2;
            s4.Release();   

        }
        private static void T5()
        {
            s4.WaitOne();   
            y *= x;
            Console.WriteLine(y);
        }

        public static void UseThreadsQueue()
        {
            Action[] tasks = new Action[] { T1, T2, T3, T4, T5 };

            Thread[] threads = new Thread[tasks.Length];
            for (int i = 0; i < tasks.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(tasks[i]));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        } 
    }

}
