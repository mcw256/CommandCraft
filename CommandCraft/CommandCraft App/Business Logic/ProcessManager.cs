using CommandCraft_App.Business_Logic.DataTypes;
using CommandCraft_App.Business_Logic.Activities;
using CommandCraft_App.Business_Logic.FileActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Business_Logic
{
    class DataContainer
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
        public List<Block> TranslatedBlocks { get; set; } = new List<Block>();
        public MyString BuildingName { get; set; } = new MyString();
    }

    public static class ProcessManager
    {
        private static DataContainer data = new DataContainer();

        private static DownloadWebSources downloadWebSources = new DownloadWebSources();
        private static ExtractBuildingName extractBuildingName = new ExtractBuildingName();
        private static ExtractBlocksFromJsContent extractBlocksFromJsContent = new ExtractBlocksFromJsContent();
        private static ExtractDictFromItemsSource extractDictFromItemsSource = new ExtractDictFromItemsSource();
        private static FindTranslationParams findTranslationParams = new FindTranslationParams();
        private static TranslateBlocks translateBlocks = new TranslateBlocks();
        private static GenerateMinecraftFunction generateMinecraftFunction = new GenerateMinecraftFunction();



        public static void SetHtmlMainUrl(string htmlMainUrl)
        {
            data.HtmlMainUrl.Value = htmlMainUrl;
            data.HtmlDictUrl.Value = @"https://www.digminecraft.com/lists/item_id_list_pc.php?fbclid=IwAR2xBKrh6ayrSUYrDLZut0IPMUH4VO_QQ0YacUGPDvIMBjiSf5zCZIGR4iA";
        }

        public static string GetBuildingName()
        {
            return data.BuildingName.Value;
        }

        public static void Process()
        {
            downloadWebSources.SetInput(data.HtmlMainUrl.Value, data.HtmlDictUrl.Value);
            downloadWebSources.SetOutput(data.HtmlDict, data.HtmlMain, data.JsUrl, data.JsContent);
            downloadWebSources.Process();

            extractBuildingName.SetInput(data.HtmlMain.Value);
            extractBuildingName.SetOutput(data.BuildingName);
            extractBuildingName.Process();

            extractBlocksFromJsContent.SetInput(data.JsContent);
            extractBlocksFromJsContent.SetOutput(data.RawBlocks);
            extractBlocksFromJsContent.Process();

            extractDictFromItemsSource.SetInput(data.HtmlDict.Value);
            extractDictFromItemsSource.SetOutput(data.ItemsDict);
            extractDictFromItemsSource.Process();

            findTranslationParams.SetInput(data.RawBlocks);
            findTranslationParams.SetOutput(data.TransParamStartingY, data.TransParamIncrease);
            findTranslationParams.Process();

            translateBlocks.SetInput(data.TransParamStartingY, data.TransParamIncrease, data.RawBlocks, data.ItemsDict);
            translateBlocks.SetOutput(data.TranslatedBlocks);
            translateBlocks.Process();

            generateMinecraftFunction.SetInput(data.TranslatedBlocks, 3, 3, 3);
            generateMinecraftFunction.SetOutput(data.MinecraftFunction);
            generateMinecraftFunction.Process();

        }
    }
}
