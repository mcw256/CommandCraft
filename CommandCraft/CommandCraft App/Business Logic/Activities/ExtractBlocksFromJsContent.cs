using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandCraft_App.Business_Logic.DataTypes;
using CommandCraft_App.Business_Logic.Utils;

namespace CommandCraft_App.Business_Logic.Activities
{

    /// <summary>
    /// Extract raw data about blocks from Js source provided in input.
    /// </summary>
    class ExtractBlocksFromJsContent
    {
        //inputs
        MyString jsContent;

        //outputs
        List<Block> rawBlocks;

        public void SetInput(MyString _jsContent)
        {
            jsContent = new MyString(_jsContent);
        }

        public void SetOutput(List<Block> _rawBlocks)
        {
            rawBlocks = _rawBlocks;
        }

        public void Process()
        {
            MatchCollection planesCol;
            MatchCollection blocksCol;

           // Console.WriteLine($"O co kurwa chodzi{jsContent.Value}");
            planesCol = RegexConfig.SelectPlane.Matches(jsContent.Value);
            
            int i = 1;
            foreach (var item in planesCol)
            {
                blocksCol = RegexConfig.SelectBlock.Matches(item.ToString()); // not sure about this convertion

                foreach (var item2 in blocksCol)
                {
                    int _x = int.Parse(RegexConfig.SelectX.Match(item2.ToString()).Value.Substring(4)); // im doing this substring because i fckd up regex 
                    int _y = int.Parse(RegexConfig.SelectY.Match(item2.ToString()).Value.Substring(4));
                    int _z = i;
                    string _type = RegexConfig.SelectType.Match(item2.ToString()).Value.Substring(5, RegexConfig.SelectType.Match(item2.ToString()).Value.Length - 6);

                    rawBlocks.Add(new Block(_x, _y, _z, _type));
                }
                i++;
            }
        }        
    }
}
