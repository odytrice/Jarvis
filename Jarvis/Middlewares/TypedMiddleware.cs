using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Middlewares
{
    public abstract class TypedMiddleware<IResultDataType> : IMiddleware
    {
        public IResult Run(string commandString, IResult previousResult)
        {
            if (previousResult is TypedResult<IResultDataType>)
            {
                return this.Run(commandString, (TypedResult<IResultDataType>)previousResult);
            }
            throw new InvalidOperationException(String.Format("Previous middleware return an IResult of {0} but this middleware expects a {1}", previousResult.GetType(),
                typeof(TypedResult<IResultDataType>).Name));
        }

        protected abstract IResult Run(string commandString, TypedResult<IResultDataType> previousResult);


        public int Priority
        {
            get;
            set;
        }
    }
}