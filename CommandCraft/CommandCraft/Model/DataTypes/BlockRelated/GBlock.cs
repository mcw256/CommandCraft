using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Model.DataTypes
{
    class GBlock : Block<BlockGInfo>
    {
        public GBlock(Coords coords, BlockGInfo blockGInfo)
        {
            Coords = coords;
            Info = blockGInfo;
        }
    }
}
