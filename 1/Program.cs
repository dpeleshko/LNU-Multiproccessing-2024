using System;
using System.Threading;

namespace MatrixExample
{
    class MainClass
    {
        static int[,] matrix;
        static int[] maxElements;
        static int M, N;

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter number of rows (M): ");
            M = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of columns (N): ");
            N = int.Parse(Console.ReadLine());

            matrix = new int[M, N];
            maxElements = new int[M];

            Random random = new Random();

            Console.WriteLine("Matrix:");
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = random.Next(1, 101); 
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Thread thread1 = new Thread(FindMaxInRows);
            thread1.Start();
            thread1.Join();  

            Thread thread2 = new Thread(CalculateAverageMax);
            thread2.Start();
            thread2.Join();  
        }

        static void FindMaxInRows()
        {
            for (int i = 0; i < M; i++)
            {
                int max = matrix[i, 0];
                for (int j = 1; j < N; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                }
                maxElements[i] = max;
                Console.WriteLine($"Max element in row {i + 1}: {max}");
            }
        }

        static void CalculateAverageMax()
        {
            double sum = 0;
            for (int i = 0; i < M; i++)
            {
                sum += maxElements[i];
            }
            double average = sum / M;
            Console.WriteLine($"Average of max elements: {average}");
        }
    }
}
