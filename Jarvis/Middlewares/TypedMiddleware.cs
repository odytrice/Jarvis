using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Middlewares
{
    public abstract class TypedMiddleware<IResultDataType> : IMiddleware
    {
        public System.Threading.Tasks.Task<IResult> Run(string commandString, IResult previousResult)
        {
            return this.Run(commandString, (TypedResult<IResultDataType>)previousResult);
        }

        protected abstract System.Threading.Tasks.Task<IResult> Run(string commandString, TypedResult<IResultDataType> previousResult);
    }
}