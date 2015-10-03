using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lab1
{
    public class Client
    {
        private IPEndPoint ipEndPoint;

        public Client(IPEndPoint ipEndPoint)
        {
            this.ipEndPoint = ipEndPoint;
        }

        public void Start()
        {
            SendMessage();
        }

        private void SendMessage()
        {
            string message = "";

            Socket sender = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);
            Console.WriteLine("Сокет соединяется с {0}", sender.RemoteEndPoint.ToString());

            while (message != "end")
            {          
                Console.Write("Введите сообщение: ");
                message = Console.ReadLine();

                byte[] bytes = new byte[1024];
                byte[] msg = Encoding.UTF8.GetBytes(message);
   
                int bytesSent = sender.Send(msg);
                int bytesRec = sender.Receive(bytes);

                Console.WriteLine("Ответ от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec)); 
            }

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
