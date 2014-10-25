using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Device
{

    public interface IDeviceProperty
    {
        object Value { get; set; }
        IEnumerable<Tag> IdTags { get; }
        IEnumerable<Tag> MutatorTags { get; }

        bool IsContinuous { get; } //convenience method for metaData == DevicePropertyType.Continuous
        bool IsDiscrete { get; } //convenience method for metaData == DevicePropertyType.Discrete
    }

    public class DeviceProperty: IDeviceProperty
    {
        public DeviceMetadata Metadata { get; private set; }

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
            get { return this.Metadata.PropertyType == DevicePropertyType.Continuous; }
        }

        public bool IsDiscrete
        {
            get { return this.Metadata.PropertyType == DevicePropertyType.Discrete; }
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

    public enum DevicePropertyType { Continuous, Discrete }

    public class DeviceMetadata
    {
        public DevicePropertyType PropertyType { get; set;}

        public IEnumerable<ValueInfo> DiscreteValues { get; set; }

        public ValueBound ContinuousBoundary { get; set; }

        public object DefaultValue { get; set; }
    }

    public class ValueInfo
    {
        public string Value { get; set; }
        public IEnumerable<Tag> ValueTags { get; set; }
    }
    public class ValueBound
    {
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
    }
}
