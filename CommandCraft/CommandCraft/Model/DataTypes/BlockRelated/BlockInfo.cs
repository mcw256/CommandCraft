using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.DataTypes
{
    abstract class BlockInfo
    {
        public virtual string Info { get; set; }
        public abstract string Name { get; set; }
        public abstract List<string> Attributes { get; set; }
        public virtual bool HasAttributes => (Attributes.Count != 0);
    }
}
