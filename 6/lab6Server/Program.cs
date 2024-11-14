using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(ipEndPoint);
        serverSocket.Listen(10);

        Console.WriteLine("Очікування вхідного TCP підключення...");
        Socket clientSocket = serverSocket.Accept();

        int[] numbers = { 34, 7, 23, 32, 5, 62 };
        Console.WriteLine("Відправлення масиву чисел клієнту: " + string.Join(", ", numbers));

        byte[] data = Encoding.UTF8.GetBytes(string.Join(",", numbers));
        clientSocket.Send(data);

        byte[] buffer = new byte[1024];
        int receivedBytes = clientSocket.Receive(buffer);
        string sortedNumbers = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

        Console.WriteLine("Відсортований масив, отриманий від клієнта: " + sortedNumbers);

        clientSocket.Close();
        serverSocket.Close();
    }
}
