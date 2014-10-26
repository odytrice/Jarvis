using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using Jarvis.Models;
using System.Threading.Tasks;

namespace Jarvis
{
    public class ClientHub : Hub
    {
        public static ConcurrentQueue<Message> Messages = new ConcurrentQueue<Message>();

        EventHandler<Service.MessageEventArgs> _handler;
        public ClientHub()
        {

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
            if (_handler == null)
            {
                _handler = Instance_NewMessageAvailable;
                Service.Events.Instance.NewMessageAvailable += _handler;
            }
            Clients.Client(Context.ConnectionId).Initialize(Messages);

        }

        public async Task Receive(Message message)
        {
            await Service.JarvisService.ToCommand(message.Body, "");
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
            if (message.Sender == "Jarvis")
            {
                Clients.All.PlayAudio(message.Body);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (_handler != null)
            {
                Service.Events.Instance.NewMessageAvailable -= _handler;
                _handler = null;
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}