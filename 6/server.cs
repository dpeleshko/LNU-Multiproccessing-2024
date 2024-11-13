using System.Net.Sockets;
using System.Net;
using System.Text;

namespace LAB6Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            tcpServer.Start();
            Console.WriteLine("Сервер запущений. Очікування підключення...");

            TcpClient tcpClient = tcpServer.AcceptTcpClient();
            Console.WriteLine("Підключення встановлено.");

            NetworkStream streamTcp = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = streamTcp.Read(buffer, 0, buffer.Length);

            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Отримане повідомлення: {receivedMessage}");
           
            string response = HasDigits(receivedMessage) ? "Yes" : "No";

            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
            streamTcp.Write(responseBuffer, 0, responseBuffer.Length);
            Console.WriteLine($"Відправлено відповідь: {response}");

            streamTcp.Close();
            tcpClient.Close();
            tcpServer.Stop();
            Console.WriteLine("Сервер завершив роботу.");
        }

        static bool HasDigits(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }
    }
}