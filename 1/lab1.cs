using System;
using System.Threading;

//1.Знайти суму елементів матриці, як суму елементів верхньої трикутної та нижньої трикутною підматриць.  
//  Матриця задається рандомно. Розмірність матриці вводиться з консолі.

namespace MatrixSumThreads
{
    class lab1
    {
        static int[,] matrix;
        static int size;
        static int upperTriangleSum = 0;
        static int lowerTriangleSum = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Введіть розмірність матриці: ");
            size = int.Parse(Console.ReadLine());

            matrix = new int[size, size];
            Random random = new Random();

            Console.WriteLine("Згенерована матриця:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = random.Next(1, 11);
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Thread upperThread = new Thread(CalculateUpperTriangleSum);
            Thread lowerThread = new Thread(CalculateLowerTriangleSum);

            upperThread.Name = "Upper Triangle Sum Thread";
            lowerThread.Name = "Lower Triangle Sum Thread";

            upperThread.Start();
            lowerThread.Start();

            upperThread.Join();
            lowerThread.Join();

            Console.WriteLine($"> Сума елементів верхньої трикутної матриці: {upperTriangleSum}");

            Console.WriteLine($"> Сума елементів нижньої трикутної матриці: {lowerTriangleSum}");

            Console.WriteLine($"> Загальна сума елементів: {upperTriangleSum + lowerTriangleSum}\n");

            Console.ReadKey();
        }
        static void CalculateUpperTriangleSum()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    upperTriangleSum += matrix[i, j];
                }
            }
            Console.WriteLine("\nОбчислення верхньої трикутної суми завершено.\n");
        }
        static void CalculateLowerTriangleSum()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    lowerTriangleSum += matrix[i, j];
                }
            }
            Console.WriteLine("Обчислення нижньої трикутної суми завершено.\n");
        }
    }
}
