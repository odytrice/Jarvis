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
            Service.Events.Instance.NewCommandReceived += Instance_NewCommandReceived;
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            return base.OnConnected();
        }

        void Instance_NewCommandReceived(object sender, Service.CommandEventArgs e)
        {
            //to do use the client id to determine which connection ids to send command to from the user id    

        }
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Receive(string message)
        {
            // 
        }
    }
}