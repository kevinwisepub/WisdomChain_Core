using System;
using WisdomCore.RPC;

namespace WisdomCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //startup service


            //init wallet

            HttpServer.StartHttpListener();
            Console.WriteLine("Wisedom startup successfully");

            Console.ReadLine();
            HttpServer.StopHttpListener();
        }
    }
}
