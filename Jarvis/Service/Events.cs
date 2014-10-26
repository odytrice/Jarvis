using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Service
{
    public class Events
    {
        public event EventHandler<CommandEventArgs> NewCommandReceived;

        public event EventHandler<ResponseEventArgs> NewResponseReceived;

        public event EventHandler<MessageEventArgs> NewMessageAvailable;

        private Events()
        {

        }

        private static Events __events;

        public static Events Instance
        {
            get
            {
                if (__events == null)
                {
                    __events = new Events();
                }
                return __events;
            }
        }

        public void DispatchCommand(ICommand command)
        {
            if (this.NewCommandReceived != null)
            {
                this.NewCommandReceived(this, new CommandEventArgs(command));
            }
        }

        public void DispatchResponse(IResponse response)
        {
            if (this.NewResponseReceived != null)
            {
                this.NewResponseReceived(this, new ResponseEventArgs(response));
            }
        }

        public void DispatchMessage(string message)
        {
            if (this.NewMessageAvailable != null)
            {
                this.NewMessageAvailable(this, new MessageEventArgs(message));
            }
        }
    }

    public class CommandEventArgs : EventArgs
    {
        public CommandEventArgs(ICommand command)
        {
            this.Command = command;
        }
        public ICommand Command { get; private set; }
    }

    public class ResponseEventArgs : EventArgs
    {
        public ResponseEventArgs(IResponse response)
        {
            this.Response = response;
        }
        public IResponse Response { get; private set; }
    }

    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string str)
        {
            this.Content = str;
        }
        public string Content { get; private set; }
    }
}