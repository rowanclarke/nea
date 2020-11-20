using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// State object for reading client data asynchronously  


namespace ODC.Connection.SocketManager
{
    public class DistributorSocketManager
    {
        private Socket listener;
        private byte[] buffer;

        public DistributorSocketManager(int port)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);

            listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
        }

        public void Listen()
        {
            
            listener.Listen(100);

        }

        public async Task<Socket> Accept()
        {
            TaskCompletionSource<Socket> tcs = new TaskCompletionSource<Socket>();
            listener.BeginAccept(
                new AsyncCallback(
                    ar =>
                    {
                        Socket handler = listener.EndAccept(ar);
                        tcs.SetResult(handler);
                    }),
                listener);
            return await tcs.Task;
        }

        public async Task<byte[]> Receive(Socket handler)
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            handler.BeginReceive(buffer, 0, 1024, SocketFlags.None,
                new AsyncCallback(
                    ar =>
                    {
                        handler.EndReceive(ar);
                        tcs.SetResult(buffer);
                    }
                    ), 
                handler);
            return await tcs.Task;
        }

        private static void Send(Socket handler, byte[] data)
        {
            handler.BeginSend(data, 0, data.Length, SocketFlags.None,
                new AsyncCallback(
                    ar =>
                    {
                        handler.EndSend(ar);
                    }
                    ), 
                handler);
        }

    }
}