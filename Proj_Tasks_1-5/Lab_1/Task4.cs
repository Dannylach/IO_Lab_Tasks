using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_1
{
    class Task4
    {
        private object _locker = new object();


        /// <summary>
        /// Problemem który próbujemy rozwiązać je operowanie kilku wątków na jednym obiekcie w tym samym czasie.
        /// Lock odpowiada za blokadę krytycznej części kodu poprzez zablokowaniu wejścia innym wątką tak długo jak jakiś działa w danym miejscu w kodzie.
        /// </summary>
        public static void Main()
        {
            ThreadPool.QueueUserWorkItem(Server);
            ThreadPool.QueueUserWorkItem(Client, "Client1");
            ThreadPool.QueueUserWorkItem(Client, "Client2");

            Thread.Sleep(5000);
        }

        private void Server(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);

            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ClientHandle, client);
            }
        }

        private void Client(Object stateInfo)
        {
            TcpClient client = new TcpClient();
            var message = new ASCIIEncoding().GetBytes((string)stateInfo);
            var buffer = new byte[1024];

            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            client.GetStream().Write(message, 0, message.Length);
            client.GetStream().Read(buffer, 0, buffer.Length);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Klient, otrzymałem wiadomość: {Encoding.UTF8.GetString(buffer)}");
        }

        public void ClientHandle(Object stateInfo)
        {
            lock (_locker)
            {
                var client = stateInfo as TcpClient;
                byte[] buffer = new byte[1024];

                client.GetStream().Read(buffer, 0, 1024);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Serwer, otrzymałem wiadomość: {Encoding.UTF8.GetString(buffer)}");

                client.GetStream().Write(buffer, 0, 1024);
                client.Close();
            }
        }
    }
}
