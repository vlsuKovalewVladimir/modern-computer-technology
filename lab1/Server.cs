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

        public async void Start()
        {
            ipEndPoint.Address = GetIPAddress();
            TcpListener listener = new TcpListener(ipEndPoint);
            listener.Start();

            Console.WriteLine("Сервер запушен: {0}", listener.Server.LocalEndPoint.ToString());

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                GetRemoteMessage(client);
            }

            //listener.Stop();
        }

        private static async void GetRemoteMessage(TcpClient c)
        {
            TcpClient client = c;
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