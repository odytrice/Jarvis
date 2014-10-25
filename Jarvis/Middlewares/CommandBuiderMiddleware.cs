using Jarvis.Core.Device;
using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Middlewares
{
    public class CommandBuiderMiddleware : TypedMiddleware<Dictionary<Device, ICollection<DeviceProperty>>>
    {
        protected override System.Threading.Tasks.Task<IResult> Run(string commandString, TypedResult<Dictionary<Device, ICollection<DeviceProperty>>> previousResult)
        {
            //previousResult.Command.Properties[0]
            return null;
        }
    }
}