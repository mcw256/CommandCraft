using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing.BlockInfosTranslatorUtils
{
    class SearchNamesDictionary
    {
        public static Dictionary<string, string> NamesDictionary { private get; set; }

        public string Output { get; private set; }
        public bool Search(string name)
        {
            if (NamesDictionary.ContainsKey(name))
            {
                Output = NamesDictionary[name];
                return true;
            }
            else
                return false;

        }


    }
}
