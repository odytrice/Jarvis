using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Service
{
    public class CommandInterpretationException : Exception
    {
        public CommandInterpretationException()
        {

        }

        public CommandInterpretationException(string message) : base(message)
        {

        }

        public CommandInterpretationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}