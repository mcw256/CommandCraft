using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Model.DataTypes
{
    abstract class Block<TInfo>
    {
        public Coords Coords { get; set; }
        public TInfo Info { get; set; } 
    }
}
