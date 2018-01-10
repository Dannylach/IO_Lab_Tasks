using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab_1
{
    class Task2
    {
        public static void Main()
        {
            ThreadPool.QueueUserWorkItem(ServerThreadProc);
            ThreadPool.QueueUserWorkItem(ClientThreadProc);
            ThreadPool.QueueUserWorkItem(ClientThreadProc);
            Thread.Sleep(20000);
        }

        private static void ServerThreadProc(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                if (client != null) ThreadPool.QueueUserWorkItem(EchoServer, client);
            }
        }

        private static void EchoServer(Object stateInfo)
        {
            TcpClient client = (TcpClient)stateInfo;
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, buffer.Length);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();
        }

        private static void ClientThreadProc(Object stateInfo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            byte[] message = System.Text.Encoding.ASCII.GetBytes(("test").ToCharArray());
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            NetworkStream stream = client.GetStream();
            stream.Write(message, 0, message.Length);
            Console.WriteLine(message);
        }
    }
}
