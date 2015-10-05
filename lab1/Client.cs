using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

using Library;

namespace lab1
{
    public class Client : BaseClient
    {
        public Client() : base() { }
        public Client(IPEndPoint ipEndPoint) : base(ipEndPoint) { }

        public override void SendMessageRemoteServer(TcpClient sender)
        {
            Console.WriteLine("Подключился к серверу: {0}", sender.Client.RemoteEndPoint.ToString());

            NetworkStream ns = sender.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            while (sender.Connected)
            {
                Console.Write("Введите сообщение: ");
                string message = Console.ReadLine();
                sw.WriteLine(message);
                sw.Flush();

                if (message == "end")
                {
                    sw.Close();
                    sr.Close();
                    sender.Close();
                }
            }
        }
    }
}
