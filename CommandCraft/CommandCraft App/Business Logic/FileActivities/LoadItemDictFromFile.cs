using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Business_Logic.FileActivities
{
    class LoadItemDictFromFile
    {
        //inputs
        string path;

        //outputs
        Dictionary<string, string> itemsDict;

        public void SetInput(string _path)
        {
            path = _path;
        }

        public void SetOutput(Dictionary<string, string> _itemsDict)
        {
            itemsDict = _itemsDict;
        }

        public void Process()
        {
            itemsDict.Clear();

            string[] translations = File.ReadAllLines(path);

            for (int i = 0; i < translations.Length; i++)
            {
                string[] keysAndValues = translations[i].Split('\t');
                itemsDict.Add(keysAndValues[0], keysAndValues[1]);
            }
        }

    }
}
