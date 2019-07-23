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
using CommandCraft_App.Model.Downloaders;
using CommandCraft_App.Model.Extractors;

namespace NameTranslationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var listOfNames = Directory.GetFiles(@"E:\Grabcraft\AllBuildingsData");

            //string result = _name.ToLower();
            //result = Regex.Replace(_name, @"\s", "_");
            //result = Regex.Replace(_name, @"[^a-z_0-9]", "");

            //return $"{result}.mcfunction";


            var download = new DownloadBuildingHtml();
            Console.WriteLine(download.Download(@"https://www.grabcraft.com/minecraft/church-of-sco-lourenco/churches").IsError);

            var extract = new ExtractBuildingName();
            Console.WriteLine(extract.Extract(download.Output).IsError);

            string result = extract.Output;

            result = result.ToLower();
            result = Regex.Replace(result, @"\s", @"_");
            result = Regex.Replace(result, @"[^a-z_0-9]", "");

            Console.WriteLine(result);


        }
    }

}

