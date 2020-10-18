using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SocketManager;

namespace Distributor
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Distributor(8080);
            Console.ReadKey();
        }
    }
}
