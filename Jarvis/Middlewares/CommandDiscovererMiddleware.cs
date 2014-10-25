using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Middlewares
{
    public class CommandDiscovererMiddleware: IMiddleware
    {
        public System.Threading.Tasks.Task<IResult> Run(string commandString, IResult previousResult)
        {
            throw new NotImplementedException();
        }
    }
}