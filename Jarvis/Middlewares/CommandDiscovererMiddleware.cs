using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Middlewares
{
    public class CommandDiscovererMiddleware : IMiddleware
    {
        public CommandDiscovererMiddleware()
        {
            this.Priority = 10; 
        }
        public IResult Run(string commandString, IResult previousResult)
        {
                if (new[] { "what", "how" }.Contains(("" + previousResult.CommandBuffer).ToLower())) previousResult.Command.CommandType = CommandType.Query;
                else previousResult.Command.CommandType = CommandType.Act;

                return previousResult;
        }

        public int Priority
        {
            get;
            set;
        }
    }
}