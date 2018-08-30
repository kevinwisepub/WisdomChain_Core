using System;
using WebSocketSharp.Server;
using WisdomCore.Core;

namespace WisdomCore.RPC
{
    public class HttpServer
    {
        static WebSocketServer wssv {get; set; } = null;
        public  HttpServer(){}

        public static void StartHttpListener()
        {
            if(Config.netType == NetType.Public_Net)
            {
                wssv = new WebSocketServer (19585);
            }
            else if(Config.netType == NetType.Test_Net)
            {
                wssv = new WebSocketServer (19586);
            }

            wssv.AddWebSocketService<RPCServer> ("/RPCServer");
            wssv.Start();
        }

        public static void StopHttpListener()
        {
            if(wssv != null) {
                wssv.Stop();
                wssv = null;
            }
        }
    }

}