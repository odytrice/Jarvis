using Jarvis.Core.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Middlewares
{
    public class DeviceLocatorMiddleware : TypedMiddleware<IEnumerable<Device>> {

        protected override System.Threading.Tasks.Task<IResult> Run(string commandString, TypedResult<IEnumerable<Device>> previousResult)
        {
            var t = new System.Threading.Tasks.Task<IResult>(() =>
            {
                var devices = previousResult.TypedData;
                if (devices == null)
                {
                    throw new NullReferenceException("no device list passed");
                }
                List<Device> discoverdDevices = new List<Device>();
                foreach (var d in devices)
                {
                    //iterate through all the tags of a device
                    foreach (var tag in d.IdTags)
                    {
                        int idx = commandString.IndexOf(tag.Name, StringComparison.CurrentCultureIgnoreCase);
                        //if the tag exists then add that device to discovered devices and remove that tag in the command string
                        if (idx != -1)
                        {
                            discoverdDevices.Add(d);
                            commandString = commandString.Remove(idx, tag.Name.Length);
                            continue;
                        }
                    }
                }
                return new TypedResult<IEnumerable<Device>>(discoverdDevices, commandString);
            });
            t.Start();
            return t;
        }
    }
}