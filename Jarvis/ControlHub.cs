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
            Service.Events.Instance.NewCommandReceived += Instance_NewCommandReceived;
        }

        void Instance_NewCommandReceived(object sender, Service.CommandEventArgs e)
        {
            Clients.All.OnCommand(e.Command.Action);
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

        public void OnCommandCompleted(string status) {
            Service.Events.Instance.DispatchResponse(new Service.Impl.JarvisResponse()
            {
                Message = status
            });
        }

        public void Hello()
        {   
            Clients.All.hello();
        }
    }
}