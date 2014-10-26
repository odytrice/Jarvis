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
        IEnumerable<DeviceProperty> Properties { get; set; }
        IEnumerable<Tag> IdTags { get; set; }

        //bool TryParse(string text, ref ICommand command);
    }

    public class Device: IDevice
    {
        public IEnumerable<DeviceProperty> Properties
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Tag[] IdTags
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
