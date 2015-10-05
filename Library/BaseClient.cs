using System;
using System.Net;
using System.Net.Sockets;

namespace Library
{
    public abstract class BaseClient : IClient
    {
        public IPEndPoint ipEndPoint { get; set; }

        public BaseClient() { }

        public BaseClient(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;
        }

        public void Start()
        {
            try
            {
                TcpClient sender = new TcpClient();
                sender.Connect(ipEndPoint);
                SendMessageRemoteServer(sender);
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка: {0}", e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        public abstract void SendMessageRemoteServer(TcpClient sender);
    }
}
