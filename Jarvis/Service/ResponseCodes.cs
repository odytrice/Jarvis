using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jarvis.Service
{
    public static class ResponseCodes
    {
        public const int DEVICE_LIST = 120;
        public const int UNKNOWN_COMMAND = -100;
        public const int COMMAND_ERROR = -400;
        public const int SUCCESS = 200;
        public const int FAILED = -500;
    }
}