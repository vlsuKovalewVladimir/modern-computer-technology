using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

using Library;

namespace lab1
{
    public class Server : IServer
    {
        private bool action;
        private TcpListener listener;

        public IPEndPoint ipEndPoint { set; get; }
       
        public Server()
        {
            action = true;
        }

        public Server(IPEndPoint ipEndPoint) : this()
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

        private async void GetRemoteMessage(TcpClient client)
        {
            string ipClient = client.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Подключился клиент: {0}", ipClient);

            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            try
            {
                string message = "";
                while (message != "end")
                {
                    message = await sr.ReadLineAsync();
                    Console.WriteLine("Клиент {0} присал сообщение: {1}", ipClient, message);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                sw.Close();
                sr.Close();
                client.Close();
                Console.WriteLine("Клиент отключился: {0}", ipClient);
            }
        }
    }  
}