using CommandCraft.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Activities
{
    /// <summary>
    /// Finds parameters that are needed to convert rawBlocks x,y,z units
    /// </summary>
    class FindTranslationParams
    {
        //inputs
        List<Block> rawBlocks;

        //outputs
        MyInt transParamStartingY;
        MyInt transParamIncrease;

        public void SetInput(List<Block> _rawBlocks)
        {
            rawBlocks = new List<Block>(_rawBlocks);
        }

        public void SetOutput(MyInt _transParamStartingY, MyInt _transParamIncrease)
        {
            transParamStartingY = _transParamStartingY;
            transParamIncrease = _transParamIncrease;
        }


        public void Process()
        {
            transParamStartingY.Value = rawBlocks.Min(x => x.Y);

            List<Block> sortedBlocks = new List<Block>(rawBlocks.OrderBy(x => x.X));

            //find minimum difference between x blocks
            int dif = int.MaxValue;

            for (int i = 1; i < sortedBlocks.Count; i++)
            {
                if ((sortedBlocks[i].X - sortedBlocks[i - 1].X) != 0)
                {
                    if ((sortedBlocks[i].X - sortedBlocks[i - 1].X) < dif)
                        dif = sortedBlocks[i].X - sortedBlocks[i - 1].X;

                }
            }

            //just to be sure, this time find minimum difference between y blocks
            sortedBlocks = new List<Block>(rawBlocks.OrderBy(x => x.Y));
            for (int i = 1; i < sortedBlocks.Count; i++)
            {
                if ((sortedBlocks[i].Y - sortedBlocks[i - 1].Y) != 0)
                {
                    if ((sortedBlocks[i].Y - sortedBlocks[i - 1].Y) < dif)
                        dif = sortedBlocks[i].Y - sortedBlocks[i - 1].Y;
                }
            }
            transParamIncrease.Value = dif;
        }      
    }
}
