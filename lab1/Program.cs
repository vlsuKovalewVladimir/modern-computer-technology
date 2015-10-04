using System;
using System.Net;

namespace lab1
{
    public enum TypeApp { Server, Client, None }
    
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                TypeApp typeApp = (args[0] == "-s") ? TypeApp.Server : (args[0] == "-c") ? TypeApp.Client : TypeApp.None;

                IPAddress ipAdress;
                IPEndPoint ipEndPoint;

                switch (typeApp)
                {
                    case TypeApp.Client:
                        ipAdress = IPAddress.Parse(args[1]);
                        ipEndPoint = new IPEndPoint(ipAdress, Int32.Parse(args[2]));
                        Client client = new Client(ipEndPoint);
                        client.Start();
                        break;

                    case TypeApp.Server:
                        ipAdress = IPAddress.Any;
                        ipEndPoint = new IPEndPoint(ipAdress, Int32.Parse(args[1]));
                        Server server = new Server(ipEndPoint);
                        server.Start();
                        break;

                    case TypeApp.None:
                        throw new Exception("Не указан тип приложения (-c или -s)");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}", e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
    }
}
