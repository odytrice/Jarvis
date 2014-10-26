using Jarvis.Core.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Middlewares
{
    public class DeviceLocatorMiddleware : TypedMiddleware<IDevice> {

        protected override System.Threading.Tasks.Task<IResult> Run(string commandString, TypedResult<IDevice> previousResult)
        {
            throw new NotImplementedException();
        }
    }
}