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
            catch(Exception e)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
