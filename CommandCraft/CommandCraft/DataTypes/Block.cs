using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.DataTypes
{
    class Block
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public string Type { get; }

        public Block(int _x, int _y, int _z, string _type)
        {
            X = _x;
            Y = _y;
            Z = _z;
            Type = _type;
        }
    }
}
