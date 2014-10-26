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
        string Id { get; set; }

        //bool TryParse(string text, ref ICommand command);c
    }

    public class Device: IDevice
    {
        private HashSet<DeviceProperty> _props = new HashSet<DeviceProperty>();
        public IEnumerable<DeviceProperty> Properties
        {
            get { return this._props; }
            set
            {
                this._props.Clear();
                if(value!=null) foreach(var prop in value) this._props.Add(prop);
            }
        }


        private HashSet<Tag> _tags = new HashSet<Tag>();
        public IEnumerable<Tag> IdTags
        {
            get { return this._tags; }
            set
            {
                this._tags.Clear();
                if (value != null) foreach (var t in value) this._tags.Add(t);
            }
        }
        public string Id { get; set; }
    }
}
