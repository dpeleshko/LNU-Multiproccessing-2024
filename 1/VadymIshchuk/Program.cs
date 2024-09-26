
using System;
using System.Threading;

class Program
{
    static int[,] matrix;
    static int upperSum = 0, lowerSum = 0; 
    static int n;

    static void Main(string[] args)
    {
        
        Console.Write("Введіть розмір матриці: ");
        n = int.Parse(Console.ReadLine());

        matrix = new int[n, n];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rand.Next(1, 10);
            }
        }


        Console.WriteLine("\nЗгенерована матриця:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }


        Thread upperThread = new Thread(CalculateUpperSum);
        Thread lowerThread = new Thread(CalculateLowerSum);

      
        upperThread.Start();
        lowerThread.Start();

        upperThread.Join();
        lowerThread.Join();




        Console.WriteLine($"\nСума елементів верхньої трикутної матриці: {upperSum}");
        Console.WriteLine($"Сума елементів нижньої трикутної матриці: {lowerSum}");
        Console.WriteLine($"Загальна сума: {upperSum + lowerSum}");
    }

    static void CalculateUpperSum()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                upperSum += matrix[i, j];
            }
        }
       
    }

   
    static void CalculateLowerSum()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                lowerSum += matrix[i, j];
            }
        }
       
    }
}

