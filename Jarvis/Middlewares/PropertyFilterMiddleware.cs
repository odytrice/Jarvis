using Jarvis.Core.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Middlewares
{
    public class PropertyFilterMiddleware: TypedMiddleware<IEnumerable<Device>>
    {
        public PropertyFilterMiddleware()
        {
            this.Priority = 900;
        }
        protected override IResult Run(string commandString, TypedResult<IEnumerable<Device>> previousResult)
        {
            if (previousResult.TypedData == null) return previousResult;
                else
                {
                    var matched = new Dictionary<Device, ICollection<DeviceProperty>>();
                    var cbuf = previousResult.CommandBuffer;
                    foreach (var device in previousResult.TypedData)
                    {
                        foreach(var prop in device.Properties)
                        {
                            //ids
                            if (prop.IdTags.Count()==0 || prop.IdTags.Any(tag => cbuf.Contains((string)tag)))
                            {
                                if (!matched.ContainsKey(device)) matched[device] = new HashSet<DeviceProperty>();
                                matched[device].Add(prop);
                            }
                        }
                    }

                    IEnumerable<string> tags = matched.Values.SelectMany(dev => dev.Select(t => t.IdTags)
                                                             .SelectMany(ts => ts.Select(t => t.Name)))
                                                             .OrderByDescending(st => st.Length);
                    tags.ToList().ForEach(p => cbuf = cbuf.Replace(p, ""));

                    return new TypedResult<Dictionary<Device, ICollection<DeviceProperty>>>(matched, cbuf)
                    {
                        Command = previousResult.Command
                    };
                }
        }
    }
}