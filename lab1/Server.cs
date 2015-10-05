using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

using Library;

namespace lab1
{
    public class Server : BaseServer
    {
        public Server() : base() { }
        public Server(IPEndPoint ipEndPoint) : base(ipEndPoint) { }
   
        public override async void GetRemoteMessage(TcpClient client)
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