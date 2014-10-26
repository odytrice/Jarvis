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
                        Message = t.Exception.Message,
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
             return Task.Run(() => Events.Instance.DispatchMessage(ResponseToString(response)));
        }

        /// <summary>
        /// Transform a list to human string form 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static String StringifyArray(Array list, Func<object, string> format = null)
        {
            System.Text.StringBuilder buff = new System.Text.StringBuilder();
            for (int i = 0, len = list.Length; i < len; i++)
            {
                if (i > 0)
                {
                    
                    if (i == len - 1)
                    {
                        buff.Append(' ').Append(Resources.Lang.AND.ToLower()).Append(' ');
                    }
                    else
                    {
                        buff.Append(", ");
                    }
                }
                if (format != null)
                {
                    buff.Append(format(list.GetValue(i)));
                }
                else
                {
                    buff.Append(list.GetValue(i).ToString());
                }
                
            }
            return buff.ToString();
        }

        public static string ResponseToString(IResponse response)
        {
            
            switch (response.StatusCode)
            {
                case ResponseCodes.SUCCESS:
                    if (response.CommandType == CommandType.Query && response.Properties != null && response.Properties.Length > 0)
                    {
                        System.Text.StringBuilder buff = new System.Text.StringBuilder();
                        bool added = false;
                        Array.ForEach(response.Properties, x =>
                        {
                            if (added)
                            {
                                buff.AppendLine();
                            }
                            if (x.Value is Array)
                            {
                                buff.AppendFormat(Resources.Lang.RESPONSE_QUERY_FORMAT, x.Name, StringifyArray((Array)x.Value));
                            }
                            else
                            {
                                buff.AppendFormat(Resources.Lang.RESPONSE_QUERY_FORMAT, x.Name, x.Value);
                            }
                            
                            added = true;
                        });
                        return buff.ToString();
                    }
                    return Resources.Lang.RESPONSE_SUCCESS;
                case ResponseCodes.FAILED:
                    return Resources.Lang.RESPONSE_FAILED;

                case ResponseCodes.COMMAND_ERROR:
                    return Resources.Lang.RESPONSE_COMMAND_ERROR;

                case ResponseCodes.UNKNOWN_COMMAND:
                    return Resources.Lang.RESPONSE_UNKNOWN_COMMAND;
                    
            }
            return response.Message;
        }
    }
}