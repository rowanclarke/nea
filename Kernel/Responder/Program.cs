using System;
using System.Threading.Tasks;
using ODC.Connection.SocketManager;

namespace ODC.Responder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            ResponderSocketManager rsm = new ResponderSocketManager(8080);
            Task co = Task.Run(rsm.Connnect);
            co.Wait();
            rsm.Send("Hello");
            Console.ReadKey();
        }
    }

}
