﻿using Jarvis.Core.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Middlewares
{
    public class ActionFilterMiddleware : TypedMiddleware<Dictionary<Device, ICollection<DeviceProperty>>>
    {
        public ActionFilterMiddleware()
        {
            this.Priority = 500;
        }
        protected override IResult Run(string commandString,
                    TypedResult<Dictionary<Device, ICollection<DeviceProperty>>> previousResult)
        {
            if (previousResult.TypedData == null) return previousResult;
            else
            {
                var matched = new Dictionary<Device, ICollection<DeviceProperty>>();
                var values = new List<String>();
                var cbuf = previousResult.CommandBuffer;
                
                foreach (var dmap in previousResult.TypedData)
                {
                    var device = dmap.Key;
                    foreach (var prop in dmap.Value)
                    {
                        //actions                        
                        if (prop.MutatorTags.Any(tag => cbuf.Contains((string)tag)))
                        {
                            if (!matched.ContainsKey(device)) matched[device] = new HashSet<DeviceProperty>();
                            matched[device].Add(prop);

                            //values
                            if(prop.IsContinuous)
                            {

                            }
                            else if(prop.IsDiscrete)
                            {
                                prop.Metadata.DiscreteValues.ToList().ForEach(v =>
                                {
                                    if (cbuf.Contains(v.Value))
                                    {
                                        values.Add(v.Value);
                                        prop.Value = v.Value;
                                    }
                                });
                            }
                        }
                    }
                }

                IEnumerable<string> tags = matched.Values.SelectMany(dev => dev.Select(t => t.IdTags)
                                                         .SelectMany(ts => ts.Select(t => t.Name)))
                                                         .OrderByDescending(st => st.Length);
                tags.ToList().ForEach(p => cbuf = cbuf.Replace(p, ""));
                values.ForEach(p =>cbuf = cbuf.Replace(p, ""));

                return new TypedResult<Dictionary<Device, ICollection<DeviceProperty>>>(matched, cbuf)
                {
                     Command = previousResult.Command
                };
            }
        }
    }
}