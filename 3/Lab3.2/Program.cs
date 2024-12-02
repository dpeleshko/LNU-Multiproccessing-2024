using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace Lab32
{
    internal class Program
    {
        static Random rand = new Random();


        static void Main(string[] args)
        {

            int numLogicalCores = Environment.ProcessorCount;
            int threadsCount;

            Console.WriteLine("Enter the number of threads (P): ");
            if (!int.TryParse(Console.ReadLine(), out threadsCount))
            {
                threadsCount = numLogicalCores;
            }
            Console.WriteLine("Enter the size of the matrices: ");
            int size;




            if (!int.TryParse(Console.ReadLine(), out size))
            {
                Console.WriteLine("Неправильне введення, використано значення за замовчуванням: 100");
                size = 100;
            }

            int[,] M1 = GenerateRandomMatrix(size);
            int[,] M2 = GenerateRandomMatrix(size);
            int[,] M3 = GenerateRandomMatrix(size);
            int[,] M4 = GenerateRandomMatrix(size);
            int[,] M5 = GenerateRandomMatrix(size);
            int[,] M6 = GenerateRandomMatrix(size);

            var stopwatch = new Stopwatch();
            stopwatch.Start();


            int[,] temp1 = MatrixAdd(MatrixAdd(M1, M2, size), M3, size); // (M1 + M2 + M3)
            int[,] temp2 = MatrixAdd(MatrixAdd(M4, M5, size), M6, size); // (M4 + M5 + M6)
            int[,] resultSingle = MatrixMultiply(temp1, temp2, size); // Result = (M1 + M2 + M3) * (M4 + M5 + M6)
            stopwatch.Stop();
            Console.WriteLine($"Size: {size}x{size}, single-threaded mode: {stopwatch.ElapsedMilliseconds} ms");

            ThreadPool.SetMaxThreads(threadsCount, threadsCount);
            stopwatch.Restart();
            temp1 = MatrixAdd(MatrixAdd(M1, M2, size), M3, size);
            temp2 = MatrixAdd(MatrixAdd(M4, M5, size), M6, size);
            int[,] resultMulti = MatrixMultiply(temp1, temp2, size);
            stopwatch.Stop();
            Console.WriteLine($"Size: {size}x{size}, {threadsCount} streams: {stopwatch.ElapsedMilliseconds} ms");
        }

        static int[,] GenerateRandomMatrix(int n)
        {
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = rand.Next(-10, 11);
            return matrix;
        }

        static int[,] MatrixAdd(int[,] M1, int[,] M2, int n)
        {
            int[,] result = new int[n, n];
            Parallel.For(0, n, i =>
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = M1[i, j] + M2[i, j];
                }
            });
            return result;
        }

        static int[,] MatrixMultiply(int[,] M1, int[,] M2, int n)
        {
            int[,] result = new int[n, n];
            Parallel.For(0, n, i =>
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        result[i, j] += M1[i, k] * M2[k, j];
                    }
                }
            });
            return result;
        }   
    }
}
