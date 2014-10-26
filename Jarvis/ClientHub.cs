using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using Jarvis.Models;

namespace Jarvis
{
    public class ClientHub : Hub
    {
        public static ConcurrentQueue<Message> Messages = new ConcurrentQueue<Message>();

        public ClientHub()
        {
            Service.Events.Instance.NewMessageAvailable += Instance_NewMessageAvailable;
        }

        void Instance_NewMessageAvailable(object sender, Service.MessageEventArgs e)
        {
            Clients.All.NewMessage(e.ToMessage());
            Messages.Enqueue(e.ToMessage());
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            NewConnection();
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            NewConnection();
            return base.OnReconnected();
        }

        private void NewConnection()
        {
            Clients.Client(Context.ConnectionId).Initialize(Messages);
        }

        public void Receive(Message message)
        {
            Service.JarvisService.ToCommand(message.Body,Context.ConnectionId);
            Messages.Enqueue(message);
            Clients.All.NewMessage(message);
        }
    }
}