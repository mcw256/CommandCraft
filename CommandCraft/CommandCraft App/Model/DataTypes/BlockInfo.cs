using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    abstract class BlockInfo
    {
        public virtual string Info { get; protected set; }
        public abstract string Name { get; protected set; }
        public abstract List<string> Attributes { get; protected set; }

    }
}
