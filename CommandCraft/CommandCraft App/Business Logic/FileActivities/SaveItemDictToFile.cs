using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Business_Logic.FileActivities
{
        static class SaveDictionaryToFile
        {
            public static void Save(Dictionary<string, string> _itemsDict, string _path) //couldnt find out how to do this through File class
            {
                string[] lines = new string[_itemsDict.Count];
                var keys = _itemsDict.Keys.ToArray();
                var values = _itemsDict.Values.ToArray();

                for (int i = 0; i < _itemsDict.Count; i++)
                    lines[i] = keys[i] + "\t" + values[i];

                File.WriteAllLines(_path, lines);

            }
        }
    
}
