using System;
using System.Threading;




namespace ScalarProductMatrix
{
    class Program
    {
       
        static int[,] matrix;
        static int numberOfRows;
        static int numberOfColumns;
        static int[] scalarProducts; 

        static void Main(string[] args)
        {
            Console.WriteLine("Введіть кількість рядків (парне число):");
            numberOfRows = int.Parse(Console.ReadLine());

            if (numberOfRows % 2 != 0)
            {
                Console.WriteLine("Кількість рядків має бути парною.");
                return;
            }

            Console.WriteLine("Введіть кількість стовпців:");
            numberOfColumns = int.Parse(Console.ReadLine());

            
            matrix = new int[numberOfRows, numberOfColumns];
            Random rand = new Random();
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    matrix[i, j] = rand.Next(1, 10); 
                }
            }

            
            Console.WriteLine("Згенерована матриця:");
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            
            scalarProducts = new int[numberOfRows / 2];

            

            
            Thread[] threads = new Thread[numberOfRows / 2];
            for (int i = 0; i < numberOfRows; i += 2)
            {
                int row1 = i;
                int row2 = i + 1;
                threads[i / 2] = new Thread(() => CalculateScalarProduct(row1, row2));
                threads[i / 2].Start();
            }

           
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

           
            Console.WriteLine("Результати скалярних добутків:");
            for (int i = 0; i < scalarProducts.Length; i++)
            {
                Console.WriteLine($"Скалярний добуток рядків {i * 2} і {i * 2 + 1}: {scalarProducts[i]}");
            }
        }

        
        static void CalculateScalarProduct(int row1, int row2)
        {
            int result = 0;
            for (int i = 0; i < numberOfColumns; i++)
            {
                result += matrix[row1, i] * matrix[row2, i];
            }

            scalarProducts[row1 / 2] = result;
        }
    }
}
