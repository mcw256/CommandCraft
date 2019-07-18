using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    abstract class BlockInfo
    {
        public string Info { get; set; }
        public abstract string Name { get; set; }
        public abstract List<string> Attributes { get; set; }

    }
}
