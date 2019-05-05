using CommandCraft_App.Business_Logic.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Business_Logic.Activities
{
    /// <summary>
    /// Creates suitable blocks data for creating minecraft function
    /// </summary>
    class TranslateBlocks
    {
        //inputs
        MyInt transParamStartingY;
        MyInt transParamIncrease;
        List<Block> rawBlocks;
        Dictionary<string, string> itemsDict;

        //outputs
        List<Block> translatedBlocks;


        public void SetInput(MyInt _transParamStartingY, MyInt _transParamIncrease, List<Block> _rawBlocks, Dictionary<string, string> _itemsDict)
        {
            transParamStartingY = new MyInt(_transParamStartingY);
            transParamIncrease = new MyInt(_transParamIncrease);
            rawBlocks = new List<Block>(_rawBlocks);
            itemsDict = new Dictionary<string, string>(_itemsDict);
        }

        public void SetOutput(List<Block> _translatedBlocks)
        {
            translatedBlocks = _translatedBlocks;
        }

        public void Process()
        {
            //Check if all types has translation, if no throw error
            foreach (var item in rawBlocks)
            {
                if (!itemsDict.ContainsKey(item.Type))
                    throw new Exception($"There is no such key as {item.Type}:(");

            }

            foreach (var item in rawBlocks)
                translatedBlocks.Add(new Block((item.X - 5) / transParamIncrease.Value, (item.Y - transParamStartingY.Value) / transParamIncrease.Value, item.Z - 1, itemsDict[item.Type]));
        }    
    }
}
