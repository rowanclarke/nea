using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using SocketManager;
namespace ODCResponder
{
    class Responder
    {
        private Socket _clientSocket;

        private byte[] _buffer;

        public delegate void OutputBuffer(byte[] buffer);

        private OutputBuffer outputBuffer;

        static void Main(string[] args)
        {

            ResponderSocketManager rsm = new ResponderSocketManager(8080);
            Task co = Task.Run(rsm.Connect);
            co.Wait();
            Console.WriteLine("Connected.");
            Console.ReadKey();
        }
    }
}
