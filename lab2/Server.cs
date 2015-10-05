using System;
using System.Net;
using System.Net.Sockets;
using Library;
using System.IO;

namespace lab2
{
    public class Server : BaseServer
    {
        public Server() : base() { }
        public Server(IPEndPoint ipEndPoint) : base(ipEndPoint) { }

        public override void GetRemoteMessage(TcpClient client)
        {
            ServerStub(client);
        }

        private async void ServerStub(TcpClient client)
        {
            string ipClient = client.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Подключился клиент: {0}", ipClient);

            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            try
            {
                string method = await sr.ReadLineAsync();
                int arg1 = int.Parse(await sr.ReadLineAsync());
                int arg2 = int.Parse(await sr.ReadLineAsync());
                int res = 0;

                switch (method)
                {
                    case "+":
                        res = add(arg1, arg2);
                        break;

                    case "-":
                        res = sub(arg1, arg2);
                        break;

                    case "*":
                        res = mul(arg1, arg2);
                        break;

                    default:
                        throw new Exception("Неверный код функции");
                }

                sw.WriteLine(res);

                Console.WriteLine("Клиент {0} присал сообщение: {1} {2} {3} | {4}", ipClient, method, arg1, arg2, res);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при обработке пакета от клиента: {0}", ipClient);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                sw.Close();
                sr.Close();
                client.Close();
                Console.WriteLine("Клиент отключился: {0}", ipClient);
            }
        }

        private int mul(int arg1, int arg2)
        {
            return arg1 * arg2;
        }

        private int sub(int arg1, int arg2)
        {
            return arg1 - arg2;
        }

        private int add(int arg1, int arg2)
        {
            return arg1 + arg2;
        }
    }
}