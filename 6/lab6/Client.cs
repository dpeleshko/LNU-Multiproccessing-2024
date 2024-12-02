using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPClient
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        TcpClient client = new TcpClient();
        client.Connect(IPAddress.Parse("127.0.0.1"), 8000);
        NetworkStream stream = client.GetStream();

        int number1 = ReadValidInteger("Введіть перше число:");
        int number2 = ReadValidInteger("Введіть друге число:");


        string message = $"{number1} {number2}";
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);

       
        byte[] buffer = new byte[256];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine("Сума чисел: " + result);

        
        stream.Close();
        client.Close();
    }

    static int ReadValidInteger(string prompt)
    {
        int number;
        while (true)
        {
            Console.WriteLine(prompt);
            if (int.TryParse(Console.ReadLine(), out number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Помилка: введене значення не є коректним числом. Спробуйте ще раз.");
            }
        }
    }
}
