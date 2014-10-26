using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Service.Impl
{
    public class JarvisResponse : IResponse
    {
        public string DeviceID
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }

        public int StatusCode
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }


        public CommandProperty[] Properties
        {
            get;
            set;
        }


        public CommandType CommandType
        {
            get;
            set;
        }
    }
}