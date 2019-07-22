using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayermapParser;
using LayermapParser.DataTypes;
using System.Text.RegularExpressions;
using NameTranslationTests.DictionaryUpdating;
using HtmlAgilityPack;

namespace NameTranslationTests
{
    class Program
    {
        static readonly string DICT_LOAD = @"E:\Grabcraft\Dictionary\digminecraftdotcom raw.json";
        static readonly string DICT_SAVE = @"E:\Grabcraft\Dictionary\digminecraftdotcom raw.json";
        static readonly string TEMP_BUILDING = @"C:\Users\rivae\Desktop\0 - Troll Watchtower.json";

        static void Main(string[] args)
        {
            Cat Tom = new Cat();

            Console.WriteLine(Tom.CatsMouse.name);

        }

        static void SerializeAndSave(Dictionary<string, string> data)
        {
            string serializedDictionary = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(DICT_SAVE, serializedDictionary);
        }

        static IEnumerable<string> LookUpDictionary(Dictionary<string,string> dict, List<string> names)
        {
            foreach (var item in names)
            {
                if (!dict.Keys.Contains(item))
                    yield return item;

            }


        }

        static Dictionary<string, string> LoadDict()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(DICT_LOAD));
        }

        static BuildingFileJsonResponse LoadBuilding()
        {
            return JsonConvert.DeserializeObject<BuildingFileJsonResponse>(File.ReadAllText(TEMP_BUILDING));
        }

        static void WhatBlocksAreMissing(BuildingFileJsonResponse building, Dictionary<string,string> blockNamesDictionary)
        {
            var blocks = LayerMapJsonParser.Parse(building.LayerMap);

            var blocknames = blocks.Select(x => x.Name).ToList<string>();

            for (int i = 0; i < blocknames.Count; i++)
                blocknames[i] = Regex.Replace(blocknames[i], @"\s\(.+\)", "");

            foreach (var item in LookUpDictionary(blockNamesDictionary, blocknames).Distinct())
            {
                Console.WriteLine(item);
            }
        }

        static void CreatePackmcmetaFile(string path)
        {
            string content = @" {
           ""pack"": {
           ""pack_format"": 3,
           ""description"": ""Test""
            }
            }";

            File.WriteAllText(path, content);
        }

    }

    static class Exceptionals
    {
        public static List<string> names = new List<string>
        {
           "dfd",
           "dfd"
        };
        public static List<string> attributes = new List<string>
        {
           "dfd",
           "dfd"
        };

        //public static bool IsExceptional(BlockGInfo info)
        //{
        //    if (names.Contains(info.Name))
        //        return true;

        //    foreach (var item in info.Attributes)
        //    {
        //        if (attributes.Contains(item))
        //            return true;
        //    }
        //    return false;
        //}

    }

    class Car
    {
        public Car(string color, int year, string name)
        {
            Color = color ?? throw new ArgumentNullException(nameof(color));
            Year = year;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Color { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        
    }

    class Cat
    {
        public Mouse CatsMouse { get; set; }

    }

    class Mouse
    {
        public Mouse()
        {
            name = "Jerry";
        }
        public string name { get; set; }
    }


}
