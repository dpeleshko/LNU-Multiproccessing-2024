namespace lab1PO
{
    internal class Program
    {
        static int n;
        static int f1Resout =0;
        static int f2Resout = 0;
        static void Main(string[] args)
        {
            
            Console.WriteLine("Imput n");
            n = Convert.ToInt32( Console.ReadLine());

            Thread thread11 = new Thread(F1);
            Thread thread12 = new Thread(F2);

            thread11.Start();
            thread12.Start();

            thread11.Join();
            thread12.Join();

            Console.WriteLine($"sum {f1Resout + f2Resout} , parne = {f1Resout}, ne parne ={f2Resout}");
            
        }

        static void F1()
        {
            int toReturn = 0;
            for(int i = 0; i <= n; i++)
            {
                if (i % 2==0)
                    f1Resout += i;
            }
        }
        static void F2()
        {
            int toReturn = 0;
            for (int i = 0; i <= n; i++)
            {
                if (i % 2 != 0)
                    f2Resout += i;
            }
        }
    }
}