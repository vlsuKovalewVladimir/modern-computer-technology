using System;
using System.Net;
using System.Net.Sockets;

namespace Library
{
    public abstract class BaseServer : IServer
    {
        private bool action;
        private TcpListener listener;

        public IPEndPoint ipEndPoint { get; set; }

        public BaseServer()
        {
            action = true;
        }

        public BaseServer(IPEndPoint ipEndPoint) : this()
        {
            this.ipEndPoint = ipEndPoint;
        }

        public async void Start()
        {
            IPEndPoint a = ipEndPoint;
            ipEndPoint.Address = ServerHelper.GetIPAddress();
            listener = new TcpListener(ipEndPoint);

            try
            {
                listener.Start();

                Console.WriteLine("Сервер запушен: {0}", listener.Server.LocalEndPoint.ToString());

                while (action)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    GetRemoteMessage(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}", e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        public void Stop()
        {
            action = false;
            Console.WriteLine("Сервер остановлен: {0}", listener.Server.LocalEndPoint.ToString());
        }

        public abstract void GetRemoteMessage(TcpClient client);
        
    }
}
