using System;
using System.Net;

namespace Library
{
    public static class ServerHelper
    {
        public static IPAddress GetIPAddress()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            int r = -1;
            bool f = true;

            while (f)
            {
                Console.WriteLine("IP адреса: ");
                for (int i = 0; i < ipHost.AddressList.Length; i++)
                    Console.WriteLine("{0}: {1}", i, ipHost.AddressList[i]);

                Console.Write("Выберите ip адрес: ");

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
