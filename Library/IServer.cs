using System.Net;

namespace Library
{
    public interface IServer
    {
        IPEndPoint ipEndPoint { get; set; }

        void Start();
        void Stop();
    }
}
