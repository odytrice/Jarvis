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
            EnqueueMessage(e.ToMessage());
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
            Service.JarvisService.ToCommand(message.Body, Context.ConnectionId);
            EnqueueMessage(message);
        }

        private void EnqueueMessage(Message message)
        {
            if (Messages.Count == 10)
            {
                Message m;
                Messages.TryDequeue(out m);
            }
            Messages.Enqueue(message);
            Clients.All.Initialize(Messages);
            Clients.All.PlayAudio(message.Body);
        }
    }
}