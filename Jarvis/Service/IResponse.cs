﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Service
{
    public interface IResponse
    {
        string DeviceID { get; set; }
        string UserID { get; set; }
        int StatusCode { get; set; }
        string Message { get; set; }
        string[] Parameters { get; set; }
    }
}
