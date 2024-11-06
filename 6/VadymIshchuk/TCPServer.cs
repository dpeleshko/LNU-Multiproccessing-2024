using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPServer
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

       
        TcpListener tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        tcpServer.Start();
        Console.WriteLine("Очікування підключення...");

    
        TcpClient tcpClient = tcpServer.AcceptTcpClient();
        Console.WriteLine("Підключення встановлено.");

      
        NetworkStream stream = tcpClient.GetStream();
        byte[] buffer = new byte[256];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Отримане повідомлення: " + message);

       
        int countA = message.Count(c => c == 'a' || c == 'A');
        Console.WriteLine("Кількість літер 'a': " + countA);

      
        byte[] response = Encoding.UTF8.GetBytes(countA.ToString());
        stream.Write(response, 0, response.Length);

      
        stream.Close();
        tcpClient.Close();
        tcpServer.Stop();
        Console.WriteLine("Завершення роботи сервера.");
    }
}
