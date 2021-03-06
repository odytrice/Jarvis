﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Message
{
    public interface ICommand
    {
        string UserID { get; set; }
        string DeviceID { get; set; }
        string Action { get; set; }
        CommandType CommandType { get; set; }

        CommandProperty[] Properties { get; set; }
    }

    public class CommandProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public enum CommandType
    {
        Act, Query, Set
    }
}
