using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LayermapParser;
using LayermapParser.DataTypes;
using Newtonsoft.Json;

namespace NameTranslationTests.DictionaryUpdating
{
    /// <summary>
    /// Main Method: Go()
    /// </summary>
    class DictionaryUpdater
    {
        Dictionary<string, string> blocksDict;
        string buildingsFolderPath;

        /// <summary>
        /// Constructor that loads dictionary
        /// </summary>
        /// <param name="pathToDictionary"></param>
        public DictionaryUpdater(string pathToDictionary)
        {
            blocksDict = LoadDictionary(pathToDictionary);
        }

        public void Go(string pathToContainingFolder, string pathWhereToSaveMissingItems)
        {
            throw new Exception("no longer need to use this");
            var fileNames = LoadListOfFileNames(pathToContainingFolder);

            List<string> missingNames = new List<string>();
            int amount = fileNames.Count;
            int i = 1;
            List<string> exceptionList = CreateExceptionsList();
            foreach (var item in fileNames)
            {
                Console.WriteLine($"{i++} out of {amount}");
                Console.WriteLine(item);
                if (exceptionList.Contains(item))
                    continue;
                var rawData = LoadBuildingData(item);
                var blocksData = ParseLayerMap(rawData.LayerMap);
                var itemNames = ExtractItemNamesList(blocksData);
                RemoveParethesesFromNames(itemNames);
                StoreMissingItems(itemNames, missingNames);
            }
            File.WriteAllLines(pathWhereToSaveMissingItems, missingNames);
        }

        public void Go2(string pathToContainingFolder)
        {
            var fileNames = LoadListOfFileNames(pathToContainingFolder);

            List<string> temporary = new List<string>();
            foreach (var item in fileNames)
            {
                var rawData = LoadBuildingData(item);
                var blocksData = ParseLayerMap(rawData.LayerMap);
                var itemNames = ExtractItemNamesList(blocksData);


                foreach (var x in itemNames)
                {
                    StringBuilder qq = new StringBuilder();

                    if (Utils.AreThereAnyAttributes(x))
                    {
                        foreach (var y in Utils.ExtractAttributesList(x))
                        {
                            qq.Append($"{y}*");
                        }
                        temporary.Add($"{ x} @@@{qq.ToString()}");

                    }
                    else
                        temporary.Add($"{ x} --- {Utils.AreThereAnyAttributes(x)}");
                    qq.Clear();
                }

                File.WriteAllLines($"{item.Substring(0, item.Length - 5)}.txt", temporary);
            }
        }

        Dictionary<string, string> LoadDictionary(string path)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));
        }

        /// <summary>
        /// Create list of file names in folder
        /// </summary>
        /// <param name="pathToContainingFolder"></param>
        /// <returns></returns>
        List<string> LoadListOfFileNames(string pathToContainingFolder)
        {
            buildingsFolderPath = pathToContainingFolder;
            return Directory.GetFiles(buildingsFolderPath).ToList<string>();
        }

        BuildingFileJsonResponse LoadBuildingData(string buildingFileName)
        {
            //string fullPath = $@"{buildingsFolderPath}\{buildingFileName}";
            string fullPath = buildingFileName;
            return JsonConvert.DeserializeObject<BuildingFileJsonResponse>(File.ReadAllText(fullPath));
        }

        List<BlockRawNamed> ParseLayerMap(string layerMap)
        {
            return LayerMapJsonParser.Parse(layerMap);
        }

        List<string> ExtractItemNamesList(List<BlockRawNamed> input)
        {
            return input.Select(x => x.Name).ToList<string>();
        }

        /// <summary>
        /// THIS FUNCTION USES PARAM AS OUTPUT
        /// </summary>
        /// <param name="reference">Used as output</param>
        void RemoveParethesesFromNames(List<string> referenceNames)
        {
            for (int i = 0; i < referenceNames.Count; i++)
                referenceNames[i] = Regex.Replace(referenceNames[i], @"\s\(.+\)", "");
        }

        /// <summary>
        /// Find items that doesnt match
        /// </summary>
        /// <param name="items"></param>
        /// <param name="storedMissingItems"></param>
        /// <returns>
        /// List of items that doesn't match any key
        /// </returns>
        IEnumerable<string> LookUpDictionary(List<string> items, List<string> storedMissingItems)
        {
            foreach (var a in items)
            {
                if ((!blocksDict.Keys.Contains(a)) && !storedMissingItems.Contains(a))
                    yield return a;
            }
        }

        /// <summary>
        /// THIS FUNCTION USES PARAM AS OUTPUT
        /// </summary>
        /// <param name="itemNames"></param>
        /// <param name="referenceMissingItems">output</param>
        void StoreMissingItems(List<string> itemNames, List<string> referenceMissingItems)
        {
            foreach (var item in LookUpDictionary(itemNames, referenceMissingItems).Distinct())
            {
                referenceMissingItems.Add(item);
            }

        }

        List<string> CreateExceptionsList()
        {
            return File.ReadAllLines(@"E:\Test\Corrupted ones.txt").ToList<string>();
        }




        static class Utils
        {
            public static bool AreThereAnyAttributes(string block)
            {
                if (Regex.IsMatch(block, @".+\(.+\)")) return true; //needs to be tested
                else return false;

            }

            public static string CutAtributes(string block)
            {
                return Regex.Replace(block, @"\s\(.+\)", "");
            }

            public static List<string> ExtractAttributesList(string block)
            {
                List<string> result = new List<string>();

                foreach (var item in Regex.Matches(block, @"[\w\s-]+[,\)]"))
                {
                    string helper = item.ToString();
                    helper = Regex.Replace(helper, @"^\s", "");
                    helper = Regex.Replace(helper, @"[,\)]$", "");

                    result.Add(helper);

                }
                return result;
            }
        }



    }
}
