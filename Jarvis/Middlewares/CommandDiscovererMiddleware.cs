using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Middlewares
{
    public class CommandDiscovererMiddleware: IMiddleware
    {
        public System.Threading.Tasks.Task<IResult> Run(string commandString, IResult previousResult)
        {
            return Task.Run<IResult>(() =>
            {
                return Task.Run<IResult>(() =>
                {
                    if (new[] { "what", "how" }.Contains(("" + previousResult.CommandBuffer).ToLower())) previousResult.Command.CommandType = CommandType.Query;
                    else previousResult.Command.CommandType = CommandType.Act;

                    return previousResult;
                });
            });
        }
    }
}