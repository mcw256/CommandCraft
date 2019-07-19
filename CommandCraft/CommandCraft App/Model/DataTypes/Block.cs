using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    abstract class Block<TInfo>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public TInfo Info { get; set; } 
    }
}
