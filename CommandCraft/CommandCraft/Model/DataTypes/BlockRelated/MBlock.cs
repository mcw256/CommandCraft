using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Model.DataTypes
{
    class MBlock : Block<BlockMInfo>
    {
        public MBlock(Coords coords, BlockMInfo info)
        {
            Coords = coords;
            Info = info;
        }

    }
}
