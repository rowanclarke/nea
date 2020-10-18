using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketManager;

namespace Responder
{


    class Program
    {
        static void Main(string[] args)
        {
            new Responder(8080);
            Console.ReadKey();
        }
    }

}
