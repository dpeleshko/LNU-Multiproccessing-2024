using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static Random rand = new Random();


    static int[,] GenerateMatrix(int size)
    {
        int[,] matrix = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = rand.Next(-10, 11);  
            }
        }
        return matrix;
    }

    static int[,] MultiplyMatrices(int[,] A, int[,] B, int numThreads)
    {
        int size = A.GetLength(0);
        int[,] result = new int[size, size];

        Parallel.For(0, size, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, i =>
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < size; k++)
                {
                    result[i, j] += A[i, k] * B[k, j];
                }
            }
        });

        return result;
    }

  
    static int[,] AddMatrices(int[,] A, int[,] B, int numThreads)
    {
        int size = A.GetLength(0);
        int[,] result = new int[size, size];

        Parallel.For(0, size, new ParallelOptions { MaxDegreeOfParallelism = numThreads }, i =>
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = A[i, j] + B[i, j];
            }
        });

        return result;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Введіть розмірність матриці N:");
        int N = int.Parse(Console.ReadLine());

        Console.WriteLine("Введіть кількість потоків:");
        int numThreads = int.Parse(Console.ReadLine());

        
        int maxThreads = Environment.ProcessorCount;
        if (numThreads > maxThreads)
        {
            Console.WriteLine($"Задана кількість потоків перевищує кількість логічних ядер ({maxThreads}). Буде використано {maxThreads} потоків.");
            numThreads = maxThreads;
        }

        int[,] M1 = GenerateMatrix(N);
        int[,] M2 = GenerateMatrix(N);
        int[,] M3 = GenerateMatrix(N);
        int[,] M4 = GenerateMatrix(N);
        int[,] M5 = GenerateMatrix(N);
        int[,] M6 = GenerateMatrix(N);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int[,] temp1 = MultiplyMatrices(M1, M2, numThreads);
        int[,] temp2 = AddMatrices(temp1, M3, numThreads);
        int[,] temp3 = MultiplyMatrices(temp2, M4, numThreads);
        int[,] temp4 = AddMatrices(temp3, M5, numThreads);
        int[,] result = AddMatrices(temp4, M6, numThreads);

        stopwatch.Stop();
        Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");

    }
}

