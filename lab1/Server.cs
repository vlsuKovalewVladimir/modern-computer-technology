using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lab1
{
    internal class Server
    {
        private IPEndPoint ipEndPoint;

        public Server(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;
        }

        public void Start()
        {
            ipEndPoint.Address = GetIPAddress();
            TcpListener listener = new TcpListener(ipEndPoint);
            listener.Start(0);

            Console.WriteLine("Сервер запушен: {0}", listener.Server.LocalEndPoint.ToString());

            TcpClient client = listener.AcceptTcpClient();
            string ipClient = client.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Подключился клиент: {0}", ipClient);

            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            while (client.Connected)
            {
                string message = sr.ReadLine();
                Console.WriteLine("Клиент {0} присал сообщение: {1}", ipClient, message);

                if (message == "end")
                {
                    sw.Close();
                    sr.Close();
                    client.Close();
                }
            }

            listener.Stop();
        }

        private IPAddress GetIPAddress()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            int r = -1;
            bool f = true;

            while (f)
            {
                Console.WriteLine("Выберите ip адрес:");

                for (int i = 0; i < ipHost.AddressList.Length; i++)
                    Console.WriteLine("{0}: {1}", i, ipHost.AddressList[i]);

                try
                {
                    r = Int32.Parse(Console.ReadLine());
                    ipAddress = ipHost.AddressList[r];
                    f = false;
                }
                catch
                {
                    Console.Clear();
                }

            }
            Console.Clear();
            return ipAddress;
        }
    }  
}