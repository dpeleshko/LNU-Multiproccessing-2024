using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MatrixComputation
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Введіть кількість потоків для виконання (P): ");
            int P = int.Parse(Console.ReadLine());

            
            int[] sizes = { 100, 500, 1000, 5000 }; 
            foreach (int N in sizes)
            {
                Console.WriteLine($"\nОбчислення для матриць розмірності: {N} x {N}");

                
                Console.WriteLine("Однопоточний режим:");
                MeasureTime(N, 1);

               
                Console.WriteLine($"\nБагатопоточний режим ({P} потоків):");
                MeasureTime(N, P);
            }
        }

        static void MeasureTime(int N, int P)
        {
            
            int[,] M1 = GenerateMatrix(N);
            int[,] M2 = GenerateMatrix(N);
            int[,] M3 = GenerateMatrix(N);
            int[,] M4 = GenerateMatrix(N);
            int[,] M5 = GenerateMatrix(N);
            int[,] M6 = GenerateMatrix(N);

            int[,] result = new int[N, N];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // (M1 + M2 * M3 + M4 + M5) * M6
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = P };

            int[,] temp1 = new int[N, N]; 
            int[,] temp2 = new int[N, N]; 
            int[,] temp3 = new int[N, N]; 

            // M2 * M3
            int[,] M2M3 = MultiplyMatrices(M2, M3, N, parallelOptions);

            // M1 + (M2 * M3)
            Parallel.For(0, N, parallelOptions, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    temp1[i, j] = M1[i, j] + M2M3[i, j];
                }
            });

            // temp1 + M4 + M5
            Parallel.For(0, N, parallelOptions, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    temp2[i, j] = temp1[i, j] + M4[i, j] + M5[i, j];
                }
            });

            // (temp2) * M6
            temp3 = MultiplyMatrices(temp2, M6, N, parallelOptions);

            stopwatch.Stop();
            Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");
        }

        static int[,] GenerateMatrix(int N)
        {
            int[,] matrix = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = rand.Next(-10, 11);
                }
            }
            return matrix;
        }

        
        static int[,] MultiplyMatrices(int[,] A, int[,] B, int N, ParallelOptions options)
        {
            int[,] result = new int[N, N];
            Parallel.For(0, N, options, i =>
            {
                for (int j = 0; j < N; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < N; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    result[i, j] = sum;
                }
            });
            return result;
        }
    }
}
