using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    static Random random = new Random();

    static int[,] GenerateMatrix(int n)
    {
        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matrix[i, j] = random.Next(-10, 11);
        return matrix;
    }

    static int[,] AddMatrices(int[,] A, int[,] B, int startRow, int endRow)
    {
        int n = A.GetLength(0);
        int[,] result = new int[n, n];

        for (int i = startRow; i < endRow; i++)
            for (int j = 0; j < n; j++)
                result[i, j] = A[i, j] + B[i, j];

        return result;
    }

    static int[,] MultiplyMatrices(int[,] A, int[,] B, int startRow, int endRow)
    {
        int n = A.GetLength(0);
        int[,] result = new int[n, n];

        for (int i = startRow; i < endRow; i++)
            for (int j = 0; j < n; j++)
                for (int k = 0; k < n; k++)
                    result[i, j] += A[i, k] * B[k, j];

        return result;
    }

    static int[,] ParallelMatrixOperation(int[,] A, int[,] B, int threadCount,
        Func<int[,], int[,], int, int, int[,]> operation)
    {
        int n = A.GetLength(0);
        int rowsPerThread = n / threadCount;
        Task<int[,]>[] tasks = new Task<int[,]>[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            int startRow = i * rowsPerThread;
            int endRow = (i == threadCount - 1) ? n : (i + 1) * rowsPerThread;

            tasks[i] = Task.Run(() => operation(A, B, startRow, endRow));
        }

        Task.WaitAll(tasks);

        int[,] result = new int[n, n];
        for (int i = 0; i < threadCount; i++)
        {
            int startRow = i * rowsPerThread;
            int endRow = (i == threadCount - 1) ? n : (i + 1) * rowsPerThread;

            for (int row = startRow; row < endRow; row++)
                for (int col = 0; col < n; col++)
                    result[row, col] = tasks[i].Result[row, col];
        }

        return result;
    }

    static void Main()
    {
        Console.Write("Введіть кількість потоків: ");
        int threadCount = int.Parse(Console.ReadLine());

        int[] dimensions = { 100, 200, 500 }; // Можна змінити на більші значення

        foreach (int n in dimensions)
        {
            Console.WriteLine($"\nРозмірність матриць: {n}x{n}");

            // Генеруємо матриці
            int[,] M1 = GenerateMatrix(n);
            int[,] M2 = GenerateMatrix(n);
            int[,] M3 = GenerateMatrix(n);
            int[,] M4 = GenerateMatrix(n);
            int[,] M5 = GenerateMatrix(n);
            int[,] M6 = GenerateMatrix(n);

            Stopwatch sw = Stopwatch.StartNew();

            // Обчислення (M1+M2)*(M3+M4)*(M5+M6)
            var sum1 = ParallelMatrixOperation(M1, M2, threadCount, AddMatrices);
            var sum2 = ParallelMatrixOperation(M3, M4, threadCount, AddMatrices);
            var sum3 = ParallelMatrixOperation(M5, M6, threadCount, AddMatrices);

            var mult1 = ParallelMatrixOperation(sum1, sum2, threadCount, MultiplyMatrices);
            var result = ParallelMatrixOperation(mult1, sum3, threadCount, MultiplyMatrices);

            sw.Stop();
            Console.WriteLine($"Час виконання з {threadCount} потоками: {sw.ElapsedMilliseconds} мс");
        }

        Console.ReadKey();
    }
}
