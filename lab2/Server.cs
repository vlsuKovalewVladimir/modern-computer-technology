using System;
using System.Net;

namespace lab2
{
    public class Server
    {
        private IPEndPoint ipEndPoint;

        public Server(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
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