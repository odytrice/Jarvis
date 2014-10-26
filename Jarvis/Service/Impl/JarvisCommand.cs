using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Service.Impl
{
    public class JarvisCommand : ICommand
    {
        public string UserID
        {
            get;
            set;
        }

        public string DeviceID
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public CommandType CommandType
        {
            get;
            set;
        }

        public string[] Parameters
        {
            get;
            set;
        }
    }
}