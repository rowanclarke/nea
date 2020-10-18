using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SocketManager
{

    public class ResponderSocketManager
    {
        private Socket clientSocket;

        private int port;

        private byte[] buffer;

        public ResponderSocketManager(int port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.port = port;

        }

        public async Task Connect()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            clientSocket.BeginConnect(new IPEndPoint(IPAddress.Loopback, port), new AsyncCallback(
                ar =>
                {
                    clientSocket.EndConnect(ar);
                    tcs.SetResult(null);
                }
                ), null);
            await tcs.Task;
        }

        public async Task<byte[]> Receive()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            buffer = new byte[clientSocket.ReceiveBufferSize];
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(
                ar =>
                {
                    clientSocket.EndReceive(ar);
                    tcs.SetResult(buffer);
                }
                ), null);

            return await tcs.Task;
        }

        public async Task Send(byte[] bytes)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(
                ar =>
                {
                    clientSocket.EndSend(ar);
                    tcs.SetResult(null);
                }

                ), null);
            await tcs.Task;
        }

    }

}
