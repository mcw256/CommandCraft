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
            //DictionaryUpdater DictUpdater = new DictionaryUpdater(@"E:\Grabcraft\Dictionary\digminecraftdotcom manual edition 1.json");

            // DictUpdater.Go(@"E:\Grabcraft\AllBuildingsData", @"E:\Test\missingItems v2.txt");

            //DictUpdater.Go3(@"E:\Grabcraft\AllBuildingsData");

            // How to set realitve path
            //string tekst = File.ReadAllText("hehe.txt");

            //var lol = JsonConvert.DeserializeObject<Dictionary<string, string>>(tekst);

            //List<string> lista = new List<string>();

            //lista.Add("adam");
            //lista.Add("marina");
            //lista.Add("wtf");
            //lista.Add("janusz");

            //File.WriteAllText("XD.txt", "Hello ladies");

            //foreach (var item in Exceptionals.names)
            //{
            //    Console.WriteLine(item);
            //}

            //List<Car> samochody = new List<Car>();
            //samochody.Add(new Car("Blue", 1992, "Mitsubishi"));
            //samochody.Add(new Car("Red", 2001, "Audi"));
            //samochody.Add(new Car("White", 2011, "Honda"));

            //foreach (var item in samochody)
            //{
            //    Console.WriteLine(item.Color);
            //}
            //Console.WriteLine("------------------");

            //List<string> kolory = samochody.Select(x => x.Color).ToList();
            //for (int i = 0; i < kolory.Count; i++)
            //{
            //    kolory[i] = $"{i} - hehe";
            //}

            //foreach (var item in kolory)
            //{
            //    Console.WriteLine(item);
            //}

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



}
