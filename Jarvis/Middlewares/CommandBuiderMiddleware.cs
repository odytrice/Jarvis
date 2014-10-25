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
            return Task.Run<IResult>(() =>
            {
                var dvs = previousResult.TypedData;
                if (dvs.Count != 1) return new TypedResult<object>(null);
                else
                {
                    previousResult.Command.DeviceID = dvs.First().Key.Id;
                    previousResult.Command.Properties = dvs.First().Value.Select(p => new CommandProperty { Name = p.Id, Value = p.Value }).ToArray();
                    previousResult.Command.Action = (string)dvs.First().Value.First().MutatorTags.First();
                    return new TypedResult<object> { Command = previousResult.Command };
                }
            });
        }
    }
}