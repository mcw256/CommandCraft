using CommandCraft.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CommandCraft
{
    class FlowData
    {
        public MyString HtmlMainUrl { get; set; } = new MyString();
        public MyString HtmlDictUrl { get; set; } = new MyString();
        public MyString HtmlDict { get; set; } = new MyString();
        public MyString HtmlMain { get; set; } = new MyString();
        public MyString JsUrl { get; set; } = new MyString();
        public MyString JsContent { get; set; } = new MyString();
        public List<Block> RawBlocks { get; set; } = new List<Block>();
        public Dictionary<string, string> ItemsDict { get; set; } = new Dictionary<string, string>();
        public MyInt TransParamStartingY { get; set; } = new MyInt();
        public MyInt TransParamIncrease { get; set; } = new MyInt();
        public List<string> MinecraftFunction { get; set; } = new List<string>();
        public List<Block> translatedBlocks { get; set; } = new List<Block>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            FlowData data = new FlowData();

            data.HtmlMainUrl.Value = @"https://www.grabcraft.com/minecraft/pig-statue/farm-animals-1";
            data.HtmlDictUrl.Value = @"https://www.digminecraft.com/lists/item_id_list_pc.php?fbclid=IwAR2xBKrh6ayrSUYrDLZut0IPMUH4VO_QQ0YacUGPDvIMBjiSf5zCZIGR4iA";

            var DownloadWebSources = new Activities.DownloadWebSources();
            var ExtractBlocksFromJsContent = new Activities.ExtractBlocksFromJsContent();
            var ExtractDictFromItemsSource = new Activities.ExtractDictFromItemsSource();
            var FindTranslationParams = new Activities.FindTranslationParams();
            var TranslateBlocks = new Activities.TranslateBlocks();
            var GenerateMinecraftFunction = new Activities.GenerateMinecraftFuntion();

            

            DownloadWebSources.SetInput(data.HtmlMainUrl.Value, data.HtmlDictUrl.Value);
            DownloadWebSources.SetOutput(data.HtmlDict, data.HtmlMain, data.JsUrl, data.JsContent);
            DownloadWebSources.Process();

            ExtractBlocksFromJsContent.SetInput(data.JsContent);
            ExtractBlocksFromJsContent.SetOutput(data.RawBlocks);
            ExtractBlocksFromJsContent.Process();

            ExtractDictFromItemsSource.SetInput(data.HtmlDict.Value);
            ExtractDictFromItemsSource.SetOutput(data.ItemsDict);
            ExtractDictFromItemsSource.Process();

            FindTranslationParams.SetInput(data.RawBlocks);
            FindTranslationParams.SetOutput(data.TransParamStartingY, data.TransParamIncrease);
            FindTranslationParams.Process();

            TranslateBlocks.SetInput(data.TransParamStartingY, data.TransParamIncrease, data.RawBlocks, data.ItemsDict);
            TranslateBlocks.SetOutput(data.translatedBlocks);
            TranslateBlocks.Process();

            GenerateMinecraftFunction.SetInput(data.translatedBlocks, 3, 3, 3);
            GenerateMinecraftFunction.SetOutput(data.MinecraftFunction);
            GenerateMinecraftFunction.Process();

            

            FileActivities.SaveMinecraftFuntionToFile.Save(data.MinecraftFunction, @"E:\Generated Function.txt");


        }
    }
}


 