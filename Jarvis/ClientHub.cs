using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Jarvis
{
    public class ClientHub : Hub
    {
        public ClientHub()
        {
            Service.Events.Instance.NewResponseReceived += Instance_NewResponseReceived;
            Service.Events.Instance.NewMessageAvailable += Instance_NewMessageAvailable;
        }

        void Instance_NewMessageAvailable(object sender, Service.MessageEventArgs e)
        {
            Clients.All.NewMessage(e.Content);
        }

        void Instance_NewResponseReceived(object sender, Service.ResponseEventArgs e)
        {
            Service.Jarvis.TranslateResponse(e.Response);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            return base.OnConnected();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void Receive(string message)
        {
            Service.Jarvis.ToCommand(message);
        }
    }
}