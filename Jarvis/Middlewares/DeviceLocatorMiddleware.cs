using Jarvis.Core.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Middlewares
{
    public class DeviceLocatorMiddleware : TypedMiddleware<IEnumerable<Device>>
    {
        public DeviceLocatorMiddleware()
        {
            this.Priority = 1000; //so that it gets 'high' on the list
        }

        protected override IResult Run(string commandString, TypedResult<IEnumerable<Device>> previousResult)
        {
            var devices = previousResult.TypedData;
            if (devices == null)
            {
                throw new NullReferenceException("no device list passed");
            }
            List<Device> discoverdDevices = new List<Device>();
            //have a list of tags that were found and later remove
            var discoveredTags = new List<string>();
            foreach (var d in devices)
            {
                //iterate through all the tags of a device
                foreach (var tag in d.IdTags)
                {
                    int idx = commandString.IndexOf(tag.Name, StringComparison.CurrentCultureIgnoreCase);
                    //if the tag exists then add that device to discovered devices and remove that tag in the command string
                    if (idx != -1)
                    {
                        discoveredTags.Add(tag.Name);
                        discoverdDevices.Add(d);
                        //commandString = commandString.Remove(idx, tag.Name.Length);
                        continue;
                    }
                }
            }
            if (discoveredTags.Count > 0)
            {
                //locate and remove from command starting with the longest tag
                foreach (var d in discoveredTags.OrderByDescending(x => x.Length))
                {
                    int idx = commandString.IndexOf(d);
                    if (idx != -1)
                    {
                        commandString = commandString.Remove(idx, d.Length);
                    }
                }
            }
            return new TypedResult<IEnumerable<Device>>(discoverdDevices, commandString) { Command = previousResult.Command };

        }
    }
}