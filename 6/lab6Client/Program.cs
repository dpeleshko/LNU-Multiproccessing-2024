using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

class Client
{
    static void Main(string[] args)
    {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));

        byte[] buffer = new byte[1024];
        int receivedBytes = clientSocket.Receive(buffer);
        int numbersString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

        int[] numbers = numbersString.Split(',').Select(int.Parse).ToArray();
        Array.Sort(numbers);

        string sortedNumbers = string.Join(",", numbers);
        Console.WriteLine("Відсортований масив чисел: " + sortedNumbers);

        byte[] sortedData = Encoding.UTF8.GetBytes(sortedNumbers);
        clientSocket.Send(sortedData);

        clientSocket.Close();
    }
}
