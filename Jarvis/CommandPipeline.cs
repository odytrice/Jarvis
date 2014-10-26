﻿using Jarvis.Core.Message;
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


        public void Add(IMiddleware middlware)
        {
            this.__middlewares.Add(middlware);
        }

        public Task<Jarvis.Core.Message.ICommand> Process(string commandString)
        {
            return new Task<Jarvis.Core.Message.ICommand>(() =>
            {
                IResult last = null;
                foreach (var p in __middlewares)
                {
                    var t = p.Run(commandString, last);
                    t.Wait();
                    last = t.Result;
                }
                if (last != null && last.Data is ICommand)
                {
                    return ((ICommand)last.Data);
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
        object Data { get; }
    }
}