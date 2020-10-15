using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace SocketManager
{
    public class DistributorSocketManager
    {
        private Socket serverSocket;
        private Socket clientSocket;

        private byte[] buffer;

        public DistributorSocketManager(int port) {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        public async Task Listen() 
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            
            
            serverSocket.Listen(0);
            serverSocket.BeginAccept(new AsyncCallback(
                ar =>
                {
                    serverSocket.EndAccept(ar);
                    tcs.SetResult(null);
                } 
                ), null);
            await tcs.Task;
        }


        public async Task<byte[]> Receive()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();

            serverSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(
                ar =>
                {
                    serverSocket.EndReceive(ar);
                    tcs.SetResult(buffer);
                }
                ), null);

            return await tcs.Task;
        }

        public async Task Send(byte[] bytes)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            serverSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(
                ar =>
                {
                    serverSocket.EndSend(ar);
                    tcs.SetResult(null);
                }
                ), null);

            await tcs.Task;
        }

        // Start Listening

    }



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
