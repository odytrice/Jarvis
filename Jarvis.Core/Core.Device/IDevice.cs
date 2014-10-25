using Jarvis.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Device
{
    public interface IDevice
    {
        IEnumerable<IDeviceProperty> Properties { get; set; }
        IEnumerable<Tag> IdTags { get; set; }

        //bool TryParse(string text, ref ICommand command);
    }
}
