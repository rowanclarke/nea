using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ODC.SocketManager;


namespace ODC.Distributor
{

    public class Distributor
    {
        private DistributorSocketManager dsm;

        public Distributor(int port)
        {
            dsm = new DistributorSocketManager(port);            
        }

        public async void DoStuff()
        {
            while (true)
            {
                await Task.Delay(1000);
                Console.Write(">");
            }
        }

        public async void Listen()
        {
            dsm.Listen();    
        }

        public async Task Accept()
        {
            Console.WriteLine("Listening...");
            Task<Socket> h = dsm.Accept();
            await h;
            Console.WriteLine("Connected.");
        }

        public async Task Receive()
        {
            Console.WriteLine("Waiting for Message...");
            Task<byte[]> re = dsm.Receive();
            await re;
            Console.WriteLine("Received.");
            Console.WriteLine(Encoding.UTF8.GetString(re.Result).Trim('\0'));
        }


    }
}
