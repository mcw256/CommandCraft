using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.DataTypes
{
    class MBlock : Block<BlockMInfo>
    {
        public MBlock(int x, int y, int z, BlockMInfo info)
        {
            X = x;
            Y = y;
            Z = z;
            Info = info;
        }

    }
}
