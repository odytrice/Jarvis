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
            
            return new Task<Jarvis.Core.Message.ICommand>(() =>
            {
                if (__middlewares.Count == 0)
                {
                    throw new ApplicationException("no middleware has been registered");
                }
                IResult last = new TypedResult<IEnumerable<Device>>(clientDevices);
                last.Command = new Jarvis.Service.Impl.JarvisCommand();
                foreach (var p in __middlewares)
                {
                    var t = p.Run(commandString, last);
                    t.Wait();
                    last = t.Result;
                }
                if (last != null && last.Command != null)
                {
                    return last.Command;
                }
                throw new ApplicationException("last middleware did not return an ICommand");
            });
        }
    }

    public interface IMiddleware
    {
        Task<IResult> Run(string commandString, IResult previousResult);
    }

    public interface IResult
    {
        string CommandBuffer { get; set; }
        object Data { get; }
        ICommand Command { get; set; }
    }
}