using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Model.Processing.BlockInfosTranslatorUtils
{
    class SearchAttributesDictionary
    {
        public static Dictionary<string, string> AttributesDictionary { private get; set; }

        public string Output { get; private set; }
        public bool Search(string name)
        {
            if (AttributesDictionary.ContainsKey(name))
            {
                Output = AttributesDictionary[name];
                return true;
            }
            else
                return false;
        }
    }
}
