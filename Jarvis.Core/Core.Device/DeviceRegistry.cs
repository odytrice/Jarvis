using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Device
{
    public class DeviceRegistry
    {
        private Dictionary<string, IEnumerable<Device>> devicesMap;
        
        private DeviceRegistry()
        {
            devicesMap = new Dictionary<string, IEnumerable<Device>>();
        }

        private static DeviceRegistry __instance;

        public static DeviceRegistry Instance
        {
            get
            {
                if (__instance == null)
                {
                    __instance = new DeviceRegistry();
                }
                return __instance;
            }
        }

        public IEnumerable<Device> Fetch(string clientID)
        {
            if (!devicesMap.ContainsKey(clientID))
            {
                return new Device[0];
            }
            return devicesMap[clientID];
        }

        public int Store(string clientID, IEnumerable<Device> devices)
        {
            devicesMap[clientID] = devices;
            return devicesMap.Count;
        }
    }
}
