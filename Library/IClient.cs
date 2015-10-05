using System.Net;

namespace Library
{
    public interface IClient
    {
        IPEndPoint ipEndPoint { get; set; }

        void Start();   
    }
}
