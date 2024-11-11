using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class server
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        TcpListener tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        tcpServer.Start();
        Console.WriteLine("Чекаємо підключення...");

        TcpClient tcpClient = tcpServer.AcceptTcpClient();
        Console.WriteLine("Підключення встановлено, вхідне повідомлення:");

        NetworkStream streamTcp = tcpClient.GetStream();

        byte[] buffer = new byte[8];
        streamTcp.Read(buffer, 0, buffer.Length);
        int number1 = BitConverter.ToInt32(buffer, 0);
        Console.WriteLine($"Отримано перше число: {number1}");

        streamTcp.Read(buffer, 0, buffer.Length); 
        int number2 = BitConverter.ToInt32(buffer, 0);
        Console.WriteLine($"Отримано друге число: {number2}");

        int product = number1 * number2;
        Console.WriteLine($"Добуток: {product}");

        byte[] resultBuffer = BitConverter.GetBytes(product);
        streamTcp.Write(resultBuffer, 0, resultBuffer.Length);
        streamTcp.Close();
        tcpClient.Close();
        tcpServer.Stop();

        Console.WriteLine("Press any key to finish...");
        Console.ReadKey();
    }
}
