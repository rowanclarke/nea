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
                    clientSocket = serverSocket.EndAccept(ar);
                    
                    Console.WriteLine("Accepted.");
                    tcs.SetResult(null);
                } 
                ), null);
            await tcs.Task;
        }


        public async Task<byte[]> Receive()
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
            Console.WriteLine("RECE1");
            buffer = new byte[clientSocket.ReceiveBufferSize];
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(
                ar =>
                {
                    
                    Console.WriteLine("RECE2");
                    clientSocket.EndReceive(ar);
                    Console.WriteLine("RECE3");

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





}
