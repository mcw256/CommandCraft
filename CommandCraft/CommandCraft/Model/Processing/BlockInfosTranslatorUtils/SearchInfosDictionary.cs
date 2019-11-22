using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Model.Processing.BlockInfosTranslatorUtils
{
    class SearchInfosDictionary
    {
        public static Dictionary<string, string> InfosDictionary { private get; set; }

        public string Output { get; private set; }
        public bool Search(string info)
        {
            if (InfosDictionary == null) return false;

            if (InfosDictionary.ContainsKey(info))
            {
                Output = InfosDictionary[info];
                return true;
            }
            else
                return false;

        }
    }
}
