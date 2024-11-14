using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPServer
{
    static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        server.Start();
        Console.WriteLine("Сервер очікує підключення...");

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Клієнт підключився.");

        NetworkStream stream = client.GetStream();
        string message = "Hello, client!";
        byte[] data = Encoding.UTF8.GetBytes(message);

        // Надсилаємо повідомлення клієнту
        stream.Write(data, 0, data.Length);
        Console.WriteLine($"Повідомлення надіслано клієнту: {message}");

        // Отримуємо відповідь від клієнта
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine($"Отримано відповідь від клієнта: {response}");

        // Закриваємо підключення
        stream.Close();
        client.Close();
        server.Stop();
        Console.WriteLine("Сервер завершив роботу.");
    }
}
