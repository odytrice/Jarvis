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
        private JarvisService()
        {
            Events.Instance.NewResponseReceived += Instance_NewResponseReceived;
        }

        void Instance_NewResponseReceived(object sender, ResponseEventArgs e)
        {
            TranslateResponse(e.Response);
        }
        /// <summary>
        /// Transforms a string, codifies into a command object that is dispatched `
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static async Task ToCommand(string str, string clientID)
        {
            try
            {
                var command = await CommandPipeline.Instance.Process(str, Jarvis.Core.Device.DeviceRegistry.Instance.Fetch(clientID));
                if (command == null)
                {
                    Events.Instance.DispatchResponse(new Impl.JarvisResponse()
                    {
                        Message = "Unknown command",
                        StatusCode = ResponseCodes.UNKNOWN_COMMAND
                    });
                }
                else
                {
                    Events.Instance.DispatchCommand(command);
                }
            }
            catch (Exception ex)
            {
                Events.Instance.DispatchResponse(new Impl.JarvisResponse()
                {
                    Message = ex.Message,
                    StatusCode = ResponseCodes.COMMAND_ERROR
                });
            }
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
        public static String StringifyArray(Array list)
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
                buff.Append(list.GetValue(i).ToString());
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