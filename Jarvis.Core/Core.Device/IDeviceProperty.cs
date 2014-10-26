using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Device
{
    public enum DevicePropertyType { Continuous, Discrete }

    public interface IDeviceProperty
    {
        DevicePropertyType PropertyType { get; }
        object Value { get; set; }
        IEnumerable<Tag> IdTags { get; }
        IEnumerable<Tag> MutatorTags { get; }

        bool IsContinuous { get; } //convenience method for metaData == DevicePropertyType.Continuous
        bool IsDiscrete { get; } //convenience method for metaData == DevicePropertyType.Discrete
    }

    public class DeviceProperty: IDeviceProperty
    {
        public DevicePropertyType PropertyType { get; set; }

        public object Value{get; set;}

        private HashSet<Tag> _idtags = new HashSet<Tag>();
        public IEnumerable<Tag> IdTags
        {
            get { return this._idtags; }
            set
            {
                this._idtags.Clear();
                if (value != null)
                    foreach (var t in value) this._idtags.Add(t);
            }
        }

        private HashSet<Tag> _mtags = new HashSet<Tag>();
        public IEnumerable<Tag> MutatorTags
        {
            get { return this._mtags; }
            set
            {
                this._mtags.Clear();
                if (value != null)
                    foreach(var t in value) this._mtags.Add(t);
            }
        }

        public bool IsContinuous
        {
            get { return this.PropertyType == DevicePropertyType.Continuous; }
        }

        public bool IsDiscrete
        {
            get { return this.PropertyType == DevicePropertyType.Discrete; }
        }


        public DeviceProperty AddIdTag(Tag tag)
        {
            this._idtags.Add(tag);
            return this;
        }
        public DeviceProperty AddMutatorTag(Tag tag)
        {
            this._mtags.Add(tag);
            return this;
        }
    }
}
