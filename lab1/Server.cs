using System;
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

            Socket listener = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                string data = "";

                listener.Bind(ipEndPoint);
                listener.Listen(10);

                Console.WriteLine("Сервер запущен, IP: {0}", ipEndPoint.ToString());
                Socket handler = listener.Accept();

                while (data != "end")
                {
                    byte[] bytes = new byte[1024];

                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("IP: {0}\t текст: {1}", handler.RemoteEndPoint.ToString(), data);

                    string reply = String.Format("Сервер получил {0} символов", data.Length.ToString());
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);            
                }

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IPAddress GetIPAddress()
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