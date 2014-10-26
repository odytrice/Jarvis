using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Message
{
    public interface IResponse
    {
        string DeviceID { get; set; }
        string UserID { get; set; }
        int StatusCode { get; set; }
        string Message { get; set; }
        CommandProperty[] Properties { get; set; }
        CommandType CommandType { get; set; }
    }
}
