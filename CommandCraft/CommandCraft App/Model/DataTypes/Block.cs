using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    abstract class Block
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public abstract BlockInfo BlockInfo { get; set; } // TODO maybe this setter shouldnt be here
    }
}
