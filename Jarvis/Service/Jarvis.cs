using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Jarvis.Service
{
    public class Jarvis
    {
        /// <summary>
        /// Transforms a string, codifies into a command object that is dispatched `
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Task ToCommand(string str)
        {
            
            return System.Threading.Tasks.Task.Run(() => {

                Events.Instance.DispatchCommand(new Impl.JarvisCommand()
                {
                    CommandType = CommandType.Act,
                    Action = str
                });
            });
            
        }


        public static Task TranslateResponse(IResponse response)
        {
             return Task.Run(() => Events.Instance.DispatchMessage(response.Message));
            
        }
    }
}