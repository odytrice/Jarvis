using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Jarvis
{
    public class ControlHub : Hub
    {
        public ControlHub()
        {
            Service.Events.Instance.NewResponseReceived += Instance_NewResponseReceived;
        }

        public override System.Threading.Tasks.Task OnConnected()
        {   
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        void Instance_NewResponseReceived(object sender, Service.ResponseEventArgs e)
        {
            
        }
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}