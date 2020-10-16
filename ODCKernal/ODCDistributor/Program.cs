using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SocketManager;

namespace ODCDistributor
{
    
    public class Distributor
    {
        private DistributorSocketManager dsm;

        static void Main(string[] args)
        {
            new Distributor(8080);
            Console.ReadKey();
        }

        public Distributor(int port)
        {
            dsm = new DistributorSocketManager(port);
            Listen();
            Console.WriteLine("Do Stuff.");
        }

        public async void Listen()
        {
            Console.WriteLine("Listening...");
            Task h = dsm.Listen();
            await h;
            
            Console.WriteLine("Connected.");
            Receive();
        } 

        public async void Receive()
        {
            Console.WriteLine("Waiting for Message...");
            Task<byte[]> re = dsm.Receive();
            await re;
            Console.WriteLine("Received.");
            Console.WriteLine(Encoding.UTF8.GetString(re.Result));
        }


    }
}
