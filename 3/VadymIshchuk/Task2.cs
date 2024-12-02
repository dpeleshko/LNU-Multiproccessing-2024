using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

class Task2
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

    static int[,] AddMatrices(int[,] M1, int[,] M2, int size)
    {
        int[,] result = new int[size, size];
        Parallel.For(0, size, i =>
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = M1[i, j] + M2[i, j];
            }
        });
        return result;
    }

    static int[,] MultiplyMatrices(int[,] M1, int[,] M2, int size)
    {
        int[,] result = new int[size, size];
        Parallel.For(0, size, i =>
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < size; k++)
                {
                    result[i, j] += M1[i, k] * M2[k, j];
                }
            }
        });
        return result;
    }

    static void Main(string[] args)
    {
        
        int numLogicalCores = Environment.ProcessorCount; 
        int numThreads;

        Console.WriteLine("Введіть кількість потоків:");
        if (!int.TryParse(Console.ReadLine(), out numThreads))
        {
            numThreads = numLogicalCores;
        }
        Console.WriteLine("Введіть розмірність матриць:");
        int size;
        if (!int.TryParse(Console.ReadLine(), out size))
        {
            Console.WriteLine("Неправильне введення, використано значення за замовчуванням: 100");
            size = 100; 
        }

        int[,] M1 = GenerateMatrix(size);
            int[,] M2 = GenerateMatrix(size);
            int[,] M3 = GenerateMatrix(size);
            int[,] M4 = GenerateMatrix(size);
            int[,] M5 = GenerateMatrix(size);
            int[,] M6 = GenerateMatrix(size);

            var stopwatch = new Stopwatch();

            
           
            stopwatch.Start();
            int[,] temp1 = AddMatrices(M1, M2, size);
            int[,] temp2 = AddMatrices(M4, AddMatrices(M5, M6, size), size);
            int[,] resultSingle = AddMatrices(temp1, MultiplyMatrices(M3, temp2, size), size);
            stopwatch.Stop();
            Console.WriteLine($"Розмір: {size}x{size}, Однопоточний режим: {stopwatch.ElapsedMilliseconds} мс");

            
            ThreadPool.SetMaxThreads(numThreads, numThreads);
            stopwatch.Restart();
            temp1 = AddMatrices(M1, M2, size);
            temp2 = AddMatrices(M4, AddMatrices(M5, M6, size), size);
            int[,] resultMulti = AddMatrices(temp1, MultiplyMatrices(M3, temp2, size), size);
            stopwatch.Stop();
            Console.WriteLine($"Розмір: {size}x{size}, {numThreads} потоків: {stopwatch.ElapsedMilliseconds} мс");
        }
    }

