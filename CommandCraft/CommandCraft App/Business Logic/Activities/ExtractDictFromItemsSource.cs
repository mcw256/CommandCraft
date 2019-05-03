using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandCraft.Business_Logic.DataTypes;
using CommandCraft.Business_Logic.Utils;

namespace CommandCraft.Business_Logic.Activities
{
    /// <summary>
    /// Extracts items dictionary from html source
    /// </summary>
    class ExtractDictFromItemsSource
    {
        //inputs
        MyString htmlDict;

        //outputs
        Dictionary<string, string> itemsDict;


        public void SetInput( string _htmlDict)
        {
            htmlDict = new MyString(_htmlDict);
        }

        public void SetOutput(Dictionary<string, string> _itemsDict)
        {
            itemsDict = _itemsDict;
        }


        public void Process()
        { 
            MatchCollection keysAndValuesCol = RegexConfig.FindKeyAndValue.Matches(htmlDict.Value);

            foreach (var item in keysAndValuesCol)
            {
                string help = RegexConfig.FindKey.Match(item.ToString()).Value;
                string key = help.Substring(1, help.Length - 5);

                help = RegexConfig.FindValue.Match(item.ToString()).Value;
                help = help.Replace("<em>", "").Replace("<wbr>", "").Replace("</em>", "");
                string value = help;
                itemsDict.Add(key, value);
            }
        }
    
    }
}
