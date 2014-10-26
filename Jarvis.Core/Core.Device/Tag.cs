using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Core.Device
{
    public class Tag
    {
        public string Name { get; set; }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.Name == (obj as Tag).Name;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }


        public static implicit operator Tag(string name)
        {
            return name!=null ? new Tag{ Name = name} : null;
        }

        public static explicit operator string(Tag t)
        {
            if(t==null) return null;
            else return t.Name;
        }

        public static bool operator ==(Tag x, Tag y)
        {
            try
            {
                return x.Equals(y);
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static bool operator !=(Tag x, Tag y)
        {
            return !(x == y);
        }
    }
}
