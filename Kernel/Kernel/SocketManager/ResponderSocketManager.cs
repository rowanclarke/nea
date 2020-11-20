using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

// State object for receiving data from remote device.  
namespace ODC.Connection.SocketManager
{
    public class ResponderSocketManager
    {
        private int port = 11000;

        private Socket client;
        private byte[] buffer;

        public ResponderSocketManager(int port)
        {
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            this.port = port;
        }

        public async void Connnect()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            client.BeginConnect(new IPEndPoint(IPAddress.Loopback, port),
                new AsyncCallback(
                    ar =>
                    {
                        client.EndConnect(ar);
                    }
                    ), client);
            await tcs.Task;
        }

        public async Task<byte[]> Receive(Socket client)
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            client.BeginReceive(buffer, 0, 1024, 0,
                new AsyncCallback(
                    ar =>
                    {
                        client.EndReceive(ar);
                        tcs.SetResult(buffer);
                    }
                    ), client);
            return await tcs.Task;
        }


        public async void Send(string data)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(
                    ar =>
                    {
                        client.EndSend(ar);
                        tcs.SetResult(null);
                    }
                    ), client);
            await tcs.Task;
        }


    }

}