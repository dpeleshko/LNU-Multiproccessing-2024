using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPServer
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        server.Start();
        Console.WriteLine("Сервер запущено. Очікування підключення...");

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Підключено клієнта.");

        NetworkStream stream = client.GetStream();

        
        byte[] buffer = new byte[256];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        
        string[] numbers = receivedData.Split(' ');
        int number1 = int.Parse(numbers[0]);
        int number2 = int.Parse(numbers[1]);

        
        int sum = number1 + number2;
        string result = sum.ToString();

        
        byte[] response = Encoding.UTF8.GetBytes(result);
        stream.Write(response, 0, response.Length);

        
        stream.Close();
        client.Close();
        server.Stop();
        Console.WriteLine("Сервер завершив роботу.");
    }
}
