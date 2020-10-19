using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ODC.SocketManager;

namespace ODC.Distributor
{
    public class Program
    {
        static void Main(string[] args)
        {
            Distributor distributor = new Distributor(8080);
            distributor.Listen();
            while (true)
            {
                Task h = Task.Run(distributor.Accept);
                h.Wait();
                Task.Run(distributor.Receive).Wait();
            }
        }
    }
}
