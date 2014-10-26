using Jarvis.Core.Device;
using Jarvis.Core.Message;
using Jarvis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis
{
    public class CommandPipeline
    {
        private List<IMiddleware> __middlewares;
        private CommandPipeline()
        {
            __middlewares = new List<IMiddleware>();
            __middlewares.Add(new Middlewares.CommandDiscovererMiddleware());
            __middlewares.Add(new Middlewares.DeviceLocatorMiddleware());
            __middlewares.Add(new Middlewares.PropertyFilterMiddleware());
            __middlewares.Add(new Middlewares.ActionFilterMiddleware());
            __middlewares.Add(new Middlewares.CommandBuiderMiddleware());
        }

        private static CommandPipeline __instance = null;

        public static CommandPipeline Instance
        {
            get
            {
                if (__instance == null)
                {
                    __instance = new CommandPipeline();
                }
                return __instance;
            }
        }


        public CommandPipeline Add(IMiddleware middlware)
        {
            this.__middlewares.Add(middlware);
            return this;
        }

        
        public Task<Jarvis.Core.Message.ICommand> Process(string commandString, IEnumerable<Device> clientDevices)
        {
            
            var t = new Task<Jarvis.Core.Message.ICommand>(() =>
            {
                if (__middlewares.Count == 0)
                {
                    throw new ApplicationException("no middleware has been registered");
                }
                IResult last = new TypedResult<IEnumerable<Device>>(clientDevices, commandString);
                last.Command = new Jarvis.Service.Impl.JarvisCommand();
                foreach (var p in __middlewares)
                {
                    if (last == null)
                    {
                        throw new NullReferenceException("last middleware did not return an IResult");
                    }
                    last = p.Run(last.CommandBuffer, last);
                }
                if (last != null && last.Command != null)
                {
                    return last.Command;
                }
                throw new ApplicationException("last middleware did not return an ICommand");
            });
            t.Start();
            return t;
        }
    }

    public interface IMiddleware
    {
        IResult Run(string commandString, IResult previousResult);
        int Priority { get; set; }
    }

    public interface IResult
    {
        string CommandBuffer { get; set; }
        object Data { get; }
        ICommand Command { get; set; }
    }
}