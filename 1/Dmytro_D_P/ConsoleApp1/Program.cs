using System;
using System.Threading;

namespace Lab1
{
    internal class Lab1
    {
        static int[,] Matrix(int m)
        {
            int[,] matrix = new int[m, m];
            Random r = new Random();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = r.Next(0, 9);
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            return matrix;
        }

        static int Scalar(int[] row_1, int[] row_2)
        {
            int size = row_1.Length;
            int s = 0;
            for (int i = 0; i < size; i++)
            {
                s += row_1[i] * row_2[i];
            }
            return s;
        }

        static int[] Row(int[,] matrix, int n)
        {
            int row = n;
            int cols = matrix.GetLength(1);
            int[] row_m = new int[cols];
            for (int i = 0; i < cols; i++)
            {
                row_m[i] = matrix[row, i];
            }
            return row_m;
        }

        static void Result(int[,] matrix, int size)
        {
            int S = size / 2;
            int l = 0;
            int[] sc_result = new int[S];
            Thread[] threads = new Thread[S];

            for (int i = 0; i < size; i += 2)
            {
                if (i + 1 >= size) { break; } 

                int[] row1 = Row(matrix, i);
                int[] row2 = Row(matrix, i + 1);
                int index = l; 

                threads[l] = new Thread(() =>
                {
                    sc_result[index] = Scalar(row1, row2);
                });
                threads[l].Start();
                l++;
            }

            
            for (int i = 0; i < l; i++)
            {
                threads[i].Join();
            }

            foreach (var result in sc_result)
            {
                Console.WriteLine(result);
            }
        }

        static void Main()
        {
            
            int size = 16; 
            int[,] m = Matrix(size);
            Result(m, size);
        }
    }
}
