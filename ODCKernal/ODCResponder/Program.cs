using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketManager;
namespace ODCResponder
{


    class Program
    {
        static void Main(string[] args)
        {
            new Responder(8080);
        }
    }

    class Responder
    {

        private ResponderSocketManager rsm;

        public Responder(int port)
        {
            rsm = new ResponderSocketManager(port);
            Connect();
            Console.ReadLine();
        }

        public async void Connect()
        {
            Console.WriteLine("Connecting...");
            await rsm.Connect();
            Console.WriteLine("Connected.");
            Send();
        }

        public async void Send()
        {
            Console.WriteLine("Sending Message...");

            Task se = rsm.Send(Encoding.UTF8.GetBytes("Hello"));

            await se;
            Console.WriteLine("Sent");
        }


        public async void Receive()
        {
            Console.WriteLine("Waiting for Message...");
            Task<byte[]> re = rsm.Receive();
            await re;
            Console.WriteLine("Received.");
            Console.WriteLine(Encoding.UTF8.GetString(re.Result));
        }
    }
}
