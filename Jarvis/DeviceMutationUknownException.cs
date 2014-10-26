using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis
{
    public class DeviceMutationUknownException : Exception
    {
        public string[] Properties { get; set; }
        public string DeviceTag { get; set; }
        public string ResidualCommand { get; set; }

        public DeviceMutationUknownException()
        {

        }
        public DeviceMutationUknownException(string message) : base(message)
        {

        }

        public DeviceMutationUknownException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}