using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPClient
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
       
        TcpClient client = new TcpClient();
        client.Connect(IPAddress.Parse("127.0.0.1"), 8000);
        NetworkStream stream = client.GetStream();

        
        Console.Write("Введіть повідомлення: ");
        string message = Console.ReadLine();
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Console.WriteLine("Повідомлення відправлено.");

        
        byte[] buffer = new byte[256];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Кількість літер 'a' у повідомленні: " + response);

       
        stream.Close();
        client.Close();
        Console.WriteLine("Завершення роботи клієнта.");
    }
}

