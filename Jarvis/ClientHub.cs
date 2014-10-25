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
        }

        void Instance_NewResponseReceived(object sender, Service.ResponseEventArgs e)
        {
            Clients.All.OnComplete(e.Response.Message);
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
            Service.Events.Instance.DispatchCommand(new Service.Impl.JarvisCommand()
            {
                Action = message
            });

        }
    }
}