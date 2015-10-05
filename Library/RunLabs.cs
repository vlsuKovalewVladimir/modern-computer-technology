using System;
using System.Net;

namespace Library
{
    public class RunLabs
    {
        public void Run(string[] args, IServer server, IClient client)
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
                        ipEndPoint = new IPEndPoint(ipAdress, int.Parse(args[2]));
                        client.ipEndPoint = ipEndPoint;
                        client.Start();
                        break;

                    case TypeApp.Server:
                        ipAdress = IPAddress.Any;
                        ipEndPoint = new IPEndPoint(ipAdress, int.Parse(args[1]));
                        server.ipEndPoint = ipEndPoint;
                        server.Start();
                        Console.ReadKey(true);
                        server.Stop();
                        break;

                    case TypeApp.None:
                        throw new Exception("Не указан тип приложения (-c или -s)");
                }

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Неверные параметры");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: {0}", e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        } 
    }
}
