using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Service
{
    public class JarvisService
    {
        /// <summary>
        /// Transforms a string, codifies into a command object that is dispatched `
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Task ToCommand(string str, string clientID)
        {
            
            return System.Threading.Tasks.Task.Run(() => {
                var t = CommandPipeline.Instance.Process(str, Jarvis.Core.Device.DeviceRegistry.Instance.Fetch(clientID));
                t.Wait();
                if (t.IsFaulted)
                {
                    Events.Instance.DispatchResponse(new Impl.JarvisResponse()
                    {
                        Message = "Command parsing failed: " + t.Exception.Message,
                        StatusCode = ResponseCodes.COMMAND_ERROR
                    });
                }
                else
                {
                    var r = t.Result;
                    if (r == null)
                    {
                        Events.Instance.DispatchResponse(new Impl.JarvisResponse()
                        {
                            Message = "Unknown command",
                            StatusCode = ResponseCodes.UNKNOWN_COMMAND
                        });
                    }
                    else
                    {
                        Events.Instance.DispatchCommand(r);
                    }
                }
            });
            
        }


        public static Task TranslateResponse(IResponse response)
        {
             return Task.Run(() => Events.Instance.DispatchMessage(response.Message));
            
        }
    }
}