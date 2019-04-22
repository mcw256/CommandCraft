using CommandCraft.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Activities
{
    /// <summary>
    /// Generates minecraft function at given x,y,z starting points
    /// </summary>
    class GenerateMinecraftFuntion
    {
        //inputs
        List<Block> translatedBlocks;
        int xStart;
        int yStart;
        int zStart;

        //outputs
        List<string> minecraftFunction;


        public void SetInput(List<Block> _translatedBlocks, int _xStart, int _yStart, int _zStart)
        {
            translatedBlocks = new List<Block>(_translatedBlocks);
            xStart = _xStart;
            yStart = _yStart;
            zStart = _zStart;
        }

        public void SetOutput(List<string> _minecraftFunction)
        {
            minecraftFunction = _minecraftFunction;
        }

        public void Process()
        {
            foreach (var item in translatedBlocks)
            {
                string singleCommand = "setblock " + Convert.ToString(item.X + xStart) + " " + Convert.ToString(item.Z + zStart) + " " + Convert.ToString(item.Y + yStart) + " " + item.Type + " replace";
                minecraftFunction.Add(singleCommand);
            }
        }
  
    }
}
