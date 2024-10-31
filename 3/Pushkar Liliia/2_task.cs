using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Вираз: (M1+M2)*(M3+M4)+M5+M6");

            while (true)
            {
                Console.Write("Введіть кількість потоків: ");
                int P = int.Parse(Console.ReadLine());

                Console.Write("Введіть розмірність матриці N (N x N): ");
                int N = int.Parse(Console.ReadLine());

                Console.WriteLine($"\nРозмірність матриці: {N}x{N}");
                int[,] M1 = GenerateMatrix(N);
                int[,] M2 = GenerateMatrix(N);
                int[,] M3 = GenerateMatrix(N);
                int[,] M4 = GenerateMatrix(N);
                int[,] M5 = GenerateMatrix(N);
                int[,] M6 = GenerateMatrix(N);

                Stopwatch stopwatch = Stopwatch.StartNew();

                int[,] result = ParallelMatrixComputation(M1, M2, M3, M4, M5, M6, P);

                stopwatch.Stop();
                Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");
                
                Console.Write("Бажаєте повторити процес? (y/n): ");
                char answer = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (char.ToLower(answer) != 'y')
                {
                    break; 
                }
                Console.WriteLine();
            }
        }

        
        static int[,] GenerateMatrix(int N)
        {
            int[,] matrix = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = random.Next(-10, 11); 
                }
            }
            return matrix;
        }
       
        static int[,] ParallelMatrixComputation(int[,] M1, int[,] M2, int[,] M3, int[,] M4, int[,] M5, int[,] M6, int P)
        {
            int N = M1.GetLength(0);
            int[,] temp1 = new int[N, N];
            int[,] temp2 = new int[N, N];
            int[,] result = new int[N, N];

            Parallel.For(0, N, new ParallelOptions { MaxDegreeOfParallelism = P }, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    temp1[i, j] = M1[i, j] + M2[i, j]; 
                    temp2[i, j] = M3[i, j] + M4[i, j];
                }
            });

            Parallel.For(0, N, new ParallelOptions { MaxDegreeOfParallelism = P }, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < N; k++)
                    {
                        sum += temp1[i, k] * temp2[k, j]; 
                    }
                    result[i, j] = sum;
                }
            });

            Parallel.For(0, N, new ParallelOptions { MaxDegreeOfParallelism = P }, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    result[i, j] += M5[i, j] + M6[i, j]; 
                }
            });

            return result;
        }
    }
}
