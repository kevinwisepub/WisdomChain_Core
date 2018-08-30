using WebSocketSharp;
using WebSocketSharp.Server;

namespace WisdomCore.RPC
{
    class RPCServer : WebSocketBehavior
    {
        protected override void OnMessage (MessageEventArgs e)
        {
            Send (e.Data);
        }
    }
}