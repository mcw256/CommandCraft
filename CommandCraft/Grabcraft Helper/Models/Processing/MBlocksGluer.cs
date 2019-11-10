using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing
{
    class MBlocksGluer : Processor<List<MBlock>, List<Coords>, List<BlockMInfo>>
    {
        public override List<MBlock> Output { get; protected set; } = new List<MBlock>();

        public override Response Process(List<Coords> coords, List<BlockMInfo> blockMInfos)
        {
            try
            {
                if (coords.Count != blockMInfos.Count)
                    throw new Exception();

                for (int i = 0; i < coords.Count; i++)
                {
                    Output.Add(new MBlock(coords[i], blockMInfos[i]));
                }
            }
            catch (Exception)
            { 
                return new Response(true, "Error");
            }
            return new Response(false, "");
        }
    }
}
