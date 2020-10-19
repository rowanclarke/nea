using ODC.SocketManager;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ODC.Responder
{

    class Responder
    {

        private ResponderSocketManager rsm;
        private string id;
        public Responder(int port)
        {
            rsm = new ResponderSocketManager(port);
            id = Guid.NewGuid().ToString();
            
        }

        

        public async void Connect()
        {
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Connecting...");
            await rsm.Connect();
            Console.WriteLine("Connected.");
        }

        public async void Send()
        {
            Console.WriteLine("Sending Message...");
            Task se = rsm.Send(Encoding.UTF8.GetBytes(id));
            await se;
            Console.WriteLine("Sent.");
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
