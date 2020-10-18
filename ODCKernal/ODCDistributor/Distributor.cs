using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketManager;


namespace Distributor
{

    public class Distributor
    {
        private DistributorSocketManager dsm;

        public Distributor(int port)
        {
            dsm = new DistributorSocketManager(port);
            Task l = Task.Run(Listen);
            Task.Run(DoStuff);
            l.Wait();
            Task re = Task.Run(Receive);
            Task.WaitAll(re);
        }

        public async void DoStuff()
        {
            while (true)
            {
                await Task.Delay(1000);
                Console.Write(">");
            }
        }

        public async Task Listen()
        {
            Console.WriteLine("Listening...");
            Task h = dsm.Listen();
            await h;

            Console.WriteLine("Connected.");
            
        }

        public async Task Receive()
        {
            Console.WriteLine("Waiting for Message...");
            Task<byte[]> re = dsm.Receive();
            await re;
            Console.WriteLine("Received.");
            Console.WriteLine(Encoding.UTF8.GetString(re.Result));
        }


    }
}
