using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace ODC.SocketManager
{
    public class DistributorSocketManager
    {
        private Socket serverSocket;
        private Socket handleSocket;

        private byte[] buffer;

        public DistributorSocketManager(int port) {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        public void Listen()
        {
            serverSocket.Listen(0);
        }

        public async Task<Socket> Accept() 
        {
            TaskCompletionSource<Socket> tcs = new TaskCompletionSource<Socket>();
            
            serverSocket.BeginAccept(new AsyncCallback(
                ar =>
                {
                    handleSocket = serverSocket.EndAccept(ar);
                    Console.WriteLine("Accepted.");
                    tcs.SetResult(handleSocket);
                } 
                ), serverSocket);
            return await tcs.Task;
        }


        public async Task<byte[]> Receive()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            buffer = new byte[handleSocket.ReceiveBufferSize];
            handleSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(
                ar =>
                {
                    handleSocket.EndReceive(ar);
                    tcs.SetResult(buffer);
                }
                ), null);

            return await tcs.Task;
        }


        // Start Listening

    }





}
