using System;
using System.Threading.Tasks;

namespace ODC.Responder
{
    class Program
    {
        static void Main(string[] args)
        {
            Responder responder = new Responder(8080);
            Task.Run(responder.Connect).Wait();
            responder.Send();
            Console.ReadKey();
        }
    }

}
