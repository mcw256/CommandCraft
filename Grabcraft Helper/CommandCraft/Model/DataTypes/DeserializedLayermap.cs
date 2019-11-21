using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.DataTypes
{
    class DeserializedLayermap
    {
        public DeserializedLayermap()
        {
            GBlocks = new List<GBlock>();
        }

        public DeserializedLayermap(List<GBlock> gBlocks, int interval)
        {
            GBlocks = gBlocks ?? throw new ArgumentNullException(nameof(gBlocks));
            Interval = interval;
        }

        public List<GBlock> GBlocks { get; set; }
        public int Interval { get; set; }

    }
}
