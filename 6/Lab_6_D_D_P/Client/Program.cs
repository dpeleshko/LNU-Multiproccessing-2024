using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPClient
{
    static void Main(string[] args)
    {
        TcpClient client = new TcpClient();
        client.Connect(IPAddress.Parse("127.0.0.1"), 8000);
        NetworkStream stream = client.GetStream();

        // Отримуємо повідомлення від сервера
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine($"Отримано повідомлення від сервера: {message}");

        // Обчислюємо кількість нулів
        int messageLength = message.Length;
        string response = new string('0', messageLength);
        byte[] responseData = Encoding.UTF8.GetBytes(response);

        // Надсилаємо кількість нулів назад серверу
        stream.Write(responseData, 0, responseData.Length);
        Console.WriteLine($"Надіслано серверу: {response}");

        // Закриваємо підключення
        stream.Close();
        client.Close();
        Console.WriteLine("Клієнт завершив роботу.");
    }
}
