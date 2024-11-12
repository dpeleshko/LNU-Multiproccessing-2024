namespace Lab1
{
    class Task5
    {
        private static int[,] matrix;
        private static int size;
        private static double mainDiagonalNorm = 0;
        private static double secondaryDiagonalNorm = 0;

        public static void SolveTask5()
        {
            GetMatrixSize();
            GenerateMatrix();
            PrintMatrix();
            CalculateDiagonalNorms();
            PrintResults();
        }

        private static void GetMatrixSize()
        {
            Console.Write("Введiть розмiрнiсть квадратної матрицi: ");
            size = int.Parse(Console.ReadLine());
        }

        private static void GenerateMatrix()
        {
            matrix = new int[size, size];
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rand.Next(1, 100);
                }
            }
        }

        private static void PrintMatrix()
        {
            Console.WriteLine("Матриця:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        private static void CalculateDiagonalNorms()
        {
            Thread mainDiagonalThread = new Thread(() => CalculateSecondaryDiagonalNorm(1));
            //Thread secondaryDiagonalThread = new Thread(CalculateSecondaryDiagonalNorm);

            mainDiagonalThread.Start();
            //secondaryDiagonalThread.Start();

            mainDiagonalThread.Join();
            //secondaryDiagonalThread.Join();
        }

        private static void CalculateMainDiagonalNorm()
        {
            double sumOfSquares = 0;
            for (int i = 0; i < size; i++)
            {
                sumOfSquares += Math.Pow(matrix[i, i], 2);
                Console.WriteLine($"Головна: {i} {sumOfSquares}");
            }
            mainDiagonalNorm = Math.Sqrt(sumOfSquares);
        }

        private static void CalculateSecondaryDiagonalNorm(int number)
        {
            Console.WriteLine(number);
            double sumOfSquares = 0;
            for (int i = 0; i < size; i++)
            {
                sumOfSquares += Math.Pow(matrix[i, size - i - 1], 2);
                Console.WriteLine($"Побiчна: {i} {sumOfSquares}");
            }
            secondaryDiagonalNorm = Math.Sqrt(sumOfSquares);
        }

        private static void PrintResults()
        {
            Console.WriteLine($"Норма вектора головної дiагоналi: {mainDiagonalNorm}");
            Console.WriteLine($"Норма вектора побiчної дiагоналi: {secondaryDiagonalNorm}");
            Console.WriteLine($"Сума норм: {mainDiagonalNorm + secondaryDiagonalNorm}");
        }
    }
}