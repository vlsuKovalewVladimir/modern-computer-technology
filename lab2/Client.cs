using System;
using System.Net;
using System.Net.Sockets;
using Library;
using System.IO;

namespace lab2
{
    public class Client : BaseClient
    {
        private TcpClient sender;

        public Client() : base() { }

        public override void SendMessageRemoteServer(TcpClient sender)
        {
            this.sender = sender;
            Console.WriteLine("Подключился к серверу: {0}", sender.Client.RemoteEndPoint.ToString());

            Console.Write("Введите код функции (+, -, *): ");
            string method = Console.ReadLine();
            Console.Write("Введите первый аргумент: ");
            int arg1 = int.Parse(Console.ReadLine());
            Console.Write("Введите второй аргумент: ");
            int arg2 = int.Parse(Console.ReadLine());
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

            Console.WriteLine("{0} {1} {2} = {3}", arg1, method, arg2, res);
        }

        private int add(int arg1, int arg2)
        {
            return ClientStub("+", arg1, arg2);
        }

        private int sub(int arg1, int arg2)
        {
            return ClientStub("-", arg1, arg2);
        }

        private int mul(int arg1, int arg2)
        {
            return ClientStub("*", arg1, arg2);
        }

        private int ClientStub(string s, int arg1, int arg2)
        {
            NetworkStream ns = sender.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            sw.WriteLine(s);
            sw.WriteLine(arg1);
            sw.WriteLine(arg2);
            sw.Flush();

            int res = int.Parse(sr.ReadLine());

            sw.Close();
            sr.Close();
            sender.Close();

            return res;
        }
    }
}