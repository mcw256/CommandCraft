using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.DataTypes
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
