using System;
using System.Threading;

class MatrixCalculation
{
    static int N = 10; // Розмірність матриць
    static int[,] M1, M2, M3, M4, M5, M6, Result1, Result2, FinalResult;
    static AutoResetEvent done1 = new AutoResetEvent(false);
    static AutoResetEvent done2 = new AutoResetEvent(false);
    static AutoResetEvent done3 = new AutoResetEvent(false);

    static Random rand = new Random();

    static void Main(string[] args)
    {
        // Ініціалізація матриць
        M1 = GenerateMatrix(N);
        M2 = GenerateMatrix(N);
        M3 = GenerateMatrix(N);
        M4 = GenerateMatrix(N);
        M5 = GenerateMatrix(N);
        M6 = GenerateMatrix(N);

        // Паралельні обчислення
        Thread t1 = new Thread(F1);
        Thread t2 = new Thread(F2);
        Thread t3 = new Thread(F3);

        t1.Start();
        t2.Start();

        t1.Join(); // Чекаємо, поки перший потік закінчить
        t2.Join(); // Чекаємо, поки другий потік закінчить

        t3.Start();
        t3.Join(); // Чекаємо, поки третій потік закінчить

        // Виведення результату
        Console.WriteLine("Фінальний результат (M1*M2 + (M3+M4+M5)*M6):");
        PrintMatrix(FinalResult);
    }

    // Множення M1 * M2
    static void F1()
    {
        Result1 = MultiplyMatrices(M1, M2);
        done1.Set(); // Сигнал про закінчення
    }

    // Сума (M3 + M4 + M5)
    static void F2()
    {
        int[,] tempSum = AddMatrices(M3, M4);
        Result2 = AddMatrices(tempSum, M5);
        done2.Set(); // Сигнал про закінчення
    }

    // Множення результату суми з M6
    static void F3()
    {
        done1.WaitOne(); // Чекаємо, поки закінчиться множення M1 * M2
        done2.WaitOne(); // Чекаємо, поки закінчиться сума (M3 + M4 + M5)

        int[,] tempMult = MultiplyMatrices(Result2, M6);
        FinalResult = AddMatrices(Result1, tempMult); // Остаточний результат
        done3.Set();
    }

    // Функція для генерування матриці з випадковими числами [-10, 10]
    static int[,] GenerateMatrix(int size)
    {
        int[,] matrix = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = rand.Next(-10, 10);
            }
        }
        return matrix;
    }

    // Додавання двох матриць
    static int[,] AddMatrices(int[,] A, int[,] B)
    {
        int[,] result = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                result[i, j] = A[i, j] + B[i, j];
            }
        }
        return result;
    }

    // Множення двох матриць
    static int[,] MultiplyMatrices(int[,] A, int[,] B)
    {
        int[,] result = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < N; k++)
                {
                    result[i, j] += A[i, k] * B[k, j];
                }
            }
        }
        return result;
    }

    // Виведення матриці на екран
    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
