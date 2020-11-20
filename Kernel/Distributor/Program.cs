using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ODC.Connection.SocketManager;

namespace ODC.Distributor
{
    public class Program
    {
        static void Main(string[] args)
        {
            DistributorSocketManager dsm = new DistributorSocketManager(8080);
            Console.WriteLine("Listening...");
            dsm.Listen();
            while (true)
            {
                Console.WriteLine("Connecting...");
                Task<Socket> h = Task.Run(dsm.Accept);
                h.Wait();

                Console.WriteLine("Connected.");
                Console.WriteLine("Receiving...");
                byte[] buffer = new byte[1024];
                Socket client = h.Result;
                Console.WriteLine(client.Connected);
                client.BeginReceive(buffer, 0, 1024, SocketFlags.None,
                    new AsyncCallback(
                        ar =>
                        {
                            client.EndReceive(ar);
                            Console.WriteLine(Encoding.UTF8.GetString(buffer));
                        }
                    ), client
                );
                
                
            }
        }
    }
}
