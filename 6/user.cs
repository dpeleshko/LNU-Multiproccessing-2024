using System.Net.Sockets;
using System.Net;
using System.Text;

namespace LAB6User
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient clientTcp = new TcpClient();
            clientTcp.Connect(IPAddress.Parse("127.0.0.1"), 8000);
            NetworkStream streamTcp = clientTcp.GetStream();

            Console.Write("Введіть повідомлення для сервера: ");
            string message = Console.ReadLine();

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            streamTcp.Write(buffer, 0, buffer.Length);

            byte[] responseBuffer = new byte[1024];
            int bytesRead = streamTcp.Read(responseBuffer, 0, responseBuffer.Length);
            string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

            Console.WriteLine($"Відповідь від сервера: {response}");

            streamTcp.Close();
            clientTcp.Close();
        }
    }
}