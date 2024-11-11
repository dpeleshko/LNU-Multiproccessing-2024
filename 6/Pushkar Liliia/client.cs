using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class client
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        TcpClient clientTcp = new TcpClient();
        clientTcp.Connect(IPAddress.Parse("127.0.0.1"), 8000);
        NetworkStream streamTcp = clientTcp.GetStream();

        int number1 = 12;
        byte[] buffer = BitConverter.GetBytes(number1);
        streamTcp.Write(buffer, 0, buffer.Length);
        Console.WriteLine($"Відправили перше число");

        int number2 = 34;
        buffer = BitConverter.GetBytes(number2);
        streamTcp.Write(buffer, 0, buffer.Length);
        Console.WriteLine($"Відправили друге число");

        buffer = new byte[4];
        streamTcp.Read(buffer, 0, buffer.Length);
        int product = BitConverter.ToInt32(buffer, 0);

        Console.WriteLine($"Отриманий результат: {product}");

        streamTcp.Close();
        clientTcp.Close();
        Console.WriteLine("Передавання завершено. Press any key to continue...");
        Console.ReadKey();
    }
}
