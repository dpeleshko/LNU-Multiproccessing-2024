using System;
using System.Security.Cryptography;
using System.Threading;

namespace Lab1
{
    internal class Program
    {

        // task 3

        static void ThreadEvenSumT3(int n)
        {
            int sum_even = 0;
            for (int i = 0; i <= n; i++)
            {
                if (i % 2 == 0)
                    sum_even += i;
            }

            Console.WriteLine($"Even sum: {sum_even}");
        }

        static void ThreadOddSumT3(int n)
        {
            int sum_odd = 0;
            for (int i = 0; i <= n; i++)
            {
                if (i % 2 == 1)
                    sum_odd += i;
            }

            Console.WriteLine($"Odd sum: {sum_odd}");
        }


        // task 4

        static void ThreadProductT4(double[] arr)
        {
            int n = arr.Length;
            double product = 1;
            for (int i = 0; i < n / 2; i++)
            {
                product *= arr[i];
            }

            Console.WriteLine($"Product n/2 = {product}");
        }

        static void ThreadSumOtherT4(double[] arr)
        {
            int n = arr.Length;
            double sum = 0;
            for (int i = n / 2; i < n; i++)
            {
                sum += arr[i];
            }

            Console.WriteLine($"Sum other n/2 = {sum}");

        }

        static double[] GenerateRealNumbers(int n, double min, double max)
        {
            Random random = new Random();
            double[] realNumbers = new double[n];

            for (int i = 0; i < n; i++)
            {
                double num = random.NextDouble() * (max - min) + min;
                realNumbers[i] = Math.Round(num, 1);
            }

            return realNumbers;
        }

        static void Main(string[] args)
        {
            // task 3

            Console.WriteLine("Input num");
            string n_str = Console.ReadLine();
            int n = Convert.ToInt32(n_str);

            Thread th1 = new Thread(() => ThreadEvenSumT3(n));
            th1.Start();
            Thread th2 = new Thread(() => ThreadOddSumT3(n));
            th2.Start();


            // task 4

            /* Console.Write("Input num ");
            string n_str = Console.ReadLine();
            int n = Convert.ToInt32(n_str);
            if (n % 2 == 1)
            {
                Console.WriteLine("Input even num");
                n_str = Console.ReadLine();
                n = Convert.ToInt32(n_str);
            }

            double[] generated_arr = GenerateRealNumbers(n, 0, 98);

            for (int i = 0; i < generated_arr.Length; i++)
                Console.Write($"{generated_arr[i]} ");

            Console.WriteLine();

            Thread th1 = new Thread(() => ThreadProductT4(generated_arr));
            th1.Start();
            Thread th2 = new Thread(() => ThreadSumOtherT4(generated_arr));
            th2.Start(); */

        }
    }
}
