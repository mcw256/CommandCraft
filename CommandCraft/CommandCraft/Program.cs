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
    class Program
    {
        static void Main(string[] args)
        {
            DownloadWebSources a = new DownloadWebSources("https://www.grabcraft.com/minecraft/pig-statue/farm-animals-1#general");
            ExtractBlocksFromJsSource b = new ExtractBlocksFromJsSource(a.jsSource);
            ExtractDictionaryFromIdsSource c = new ExtractDictionaryFromIdsSource(a.htmlIdsSource);
            FindTranslationParams d = new FindTranslationParams(b.Blocks);
            TranslateBlockListCoordinates e = new TranslateBlockListCoordinates(d.StartingY, d.Increase, b.Blocks, c.TypeTranslations);
            GenerateMinecraftFuntion f = new GenerateMinecraftFuntion(e.TranslatedBlocks, 200, 65, 300);
            SaveMinecraftFuntionToFile.Save(f.MinecraftFunction, @"E:\Generated Function.txt");



        }
    }

    /// <summary>
    /// Downloads both html and js source from url given in _address.
    /// Additionaly downloads minecraft ids from site specified inside, and creates dictionary for item names to minecraft ids translation
    /// </summary>
    /// <remarks>
    /// js source is file that describes LayerMap bluprint on grabcraft
    /// </remarks>
    class DownloadWebSources
    {
        public string htmlIdsSource { get; }
        public string htmlSource { get; }
        public string jsSource { get; }


        public DownloadWebSources(string _address)
        {
            using (var site = new ImprovedWebClient())
            {
                site.Headers[HttpRequestHeader.UserAgent] = "Mozilla /5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                htmlSource = site.DownloadString(_address);
                site.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                jsSource = site.DownloadString(DownloadJsUrl());
                htmlIdsSource = site.DownloadString("https://www.digminecraft.com/lists/item_id_list_pc.php?fbclid=IwAR2xBKrh6ayrSUYrDLZut0IPMUH4VO_QQ0YacUGPDvIMBjiSf5zCZIGR4iA");
            }
        }
        private string DownloadJsUrl()
        {
            Regex findJsAddress = new Regex(@"https.+\/LayerMap\/.+\.js"); //<script src="https://wwwgrabcraftcom-jzoul76nauwdp2hxnkfs.stackpathdns.com/js/LayerMap/LayerMap_1225.js">    
            string url = findJsAddress.Match(htmlSource).Value;
            return url;
        }

    }


    /// <summary>
    /// Extracts planes amount, x-size and y-size from html source
    /// </summary>
    /// <remarks>
    /// This class should be used mostly to check if there was any errors in further regex jsSource operations
    /// </remarks>
    class ExtractValuesFromHtmlSource
    {
        public int PlanesAmount { get; }
        public int XSize { get; }
        public int YSize { get; }

        public ExtractValuesFromHtmlSource(string _htmlSource)
        {
            string begining = "\"value dimension-n\">";
            string end = "</td>";

            string _planesAmount;
            string _xSize;
            string _ySize;

            Regex findPlanesAmount = new Regex("\"value dimension-y\">\\d+<\\/td>");
            Regex findXSize = new Regex("\"value dimension-x\">\\d+<\\/td>"); // im not quite sure if x is x here
            Regex findYSize = new Regex("\"value dimension-z\">\\d+<\\/td>");

            _planesAmount = findPlanesAmount.Match(_htmlSource).Value;
            _planesAmount = _planesAmount.Substring(begining.Length, _planesAmount.Length - begining.Length - end.Length);

            _xSize = findXSize.Match(_htmlSource).Value;
            _xSize = _xSize.Substring(begining.Length, _xSize.Length - begining.Length - end.Length);

            _ySize = findYSize.Match(_htmlSource).Value;
            _ySize = _ySize.Substring(begining.Length, _ySize.Length - begining.Length - end.Length);


            PlanesAmount = int.Parse(_planesAmount);
            XSize = int.Parse(_xSize);
            YSize = int.Parse(_ySize);

        }
    }


    /// <summary>
    /// This class defines Block data type
    /// </summary>
    /// <remarks>
    /// Each block contains x,y,z and type, which are coordinates of block and its type
    /// </remarks>
    class Block
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public string Type { get; }

        public Block(int _x, int _y, int _z, string _type)
        {
            X = _x;
            Y = _y;
            Z = _z;
            Type = _type;
        }
    }


    /// <summary>
    /// This class extracts data about blocks from javascript source string passed by constructor param _jsSource
    /// </summary>
    class ExtractBlocksFromJsSource
    {
        public List<Block> Blocks { get; }

        public ExtractBlocksFromJsSource(string _jsSource)
        {
            Regex selectPlane = new Regex("\"\\d+\":\\[[{}\"\\w:,()-\\\\\\s]+\\]");
            Regex selectBlock = new Regex(@"{[""\w:,\s()-]+}");
            Regex selectX = new Regex(@"""x"":\d+");
            Regex selectY = new Regex(@"""y"":\d+");
            Regex selectType = new Regex(@"""h"":""[\w\s()-,]+"""); // "h":"[\w\s()-,]+"

            MatchCollection planesCol;
            MatchCollection blocksCol;

            planesCol = selectPlane.Matches(_jsSource);
            Blocks = new List<Block>();
            int i = 1;
            foreach (var item in planesCol)
            {
                blocksCol = selectBlock.Matches(item.ToString()); // not sure about this convertion

                foreach (var item2 in blocksCol)
                {
                    int _x = int.Parse(selectX.Match(item2.ToString()).Value.Substring(4)); // im doing this substring because i fckd up regex 
                    int _y = int.Parse(selectY.Match(item2.ToString()).Value.Substring(4));
                    int _z = i;
                    string _type = selectType.Match(item2.ToString()).Value.Substring(5, selectType.Match(item2.ToString()).Value.Length - 6);

                    Blocks.Add(new Block(_x, _y, _z, _type));
                }
                i++;
            }
        }
    }

    /// <summary>
    /// Downloads pair Item_Name, Minecraft_Id from given html source of site and creates dictionary from it
    /// </summary>
    class ExtractDictionaryFromIdsSource
    {
        public Dictionary<string, string> TypeTranslations { get; }

        public ExtractDictionaryFromIdsSource(string _itemsIdsSiteSource)
        {
            TypeTranslations = new Dictionary<string, string>();

            Regex findKeyAndValue = new Regex(@">[\w\s]+<\/a><br>\(<em>minecraft:[(<wbr>\w_]+<\/em>"); // >Acacia Boat</a><br>(<em>minecraft:<wbr>acacia_<wbr>boat</em>
            Regex findKey = new Regex(@">[\w\s]+<\/a>"); //>Acacia Boat</a>
            Regex findValue = new Regex(@"<em>minecraft:<wbr>[\w(<wbr>)]+<\/em>"); //<em>minecraft:<wbr>acacia_<wbr>boat</em>

            MatchCollection keysAndValuesCol = findKeyAndValue.Matches(_itemsIdsSiteSource);

            foreach (var item in keysAndValuesCol)
            {
                string help = findKey.Match(item.ToString()).Value;
                string key = help.Substring(1, help.Length - 5);

                help = findValue.Match(item.ToString()).Value;
                help = help.Replace("<em>", "").Replace("<wbr>", "").Replace("</em>", "");
                string value = help;
                TypeTranslations.Add(key, value);
            }
        }
    }


    /// <summary>
    /// No need to explain
    /// </summary>
    class LoadDictionaryFromFile
    {
        Dictionary<string, string> TypeTranslations { get; }
        public LoadDictionaryFromFile(string _path)
        {
            TypeTranslations = new Dictionary<string, string>();
            string[] translations = File.ReadAllLines(_path);

            for (int i = 0; i < translations.Length; i++)
            {
                string[] keysAndValues = translations[i].Split('\t');
                TypeTranslations.Add(keysAndValues[0], keysAndValues[1]);
            }
        }
    }

    /// <summary>
    /// No need to explain
    /// </summary>
    static class SaveDictionaryToFile
    {
        public static void Save(Dictionary<string, string> TypeTranslations, string _path) //couldnt find out how to do this through File class
        {
            string[] lines = new string[TypeTranslations.Count];
            var keys = TypeTranslations.Keys.ToArray();
            var values = TypeTranslations.Values.ToArray();

            for (int i = 0; i < TypeTranslations.Count; i++)
                lines[i] = keys[i] + "\t" + values[i];

            File.WriteAllLines(_path, lines);

        }
    }

    /// <summary>
    /// Translate blocks coordinates, so minecraft function can be made
    /// </summary>
    class TranslateBlockListCoordinates
    {
        public List<Block> TranslatedBlocks { get; }


        public TranslateBlockListCoordinates(int _startingY, int _increase, List<Block> Blocks, Dictionary<string, string> TypeTranslations)
        {
            TranslatedBlocks = new List<Block>();

            //Check if all types has translation, if no throw error
            foreach (var item in Blocks)
            {
                if (!TypeTranslations.ContainsKey(item.Type))
                    throw new Exception($"There is no such key as {item.Type}:(");

            }

            foreach (var item in Blocks)
                TranslatedBlocks.Add(new Block((item.X - 5) / _increase, (item.Y - _startingY) / _increase, item.Z - 1, TypeTranslations[item.Type]));
        }
    }


    /// <summary>
    /// Finds translation params; starting y and increase, which are needed for translating blocks coordinates
    /// </summary>
    class FindTranslationParams
    {
        public int StartingY { get; }
        public int Increase { get; }

        public FindTranslationParams(List<Block> Blocks)
        {
            //find minimum y
            StartingY = Blocks.Min(x => x.Y);

            List<Block> sortedBlocks = new List<Block>(Blocks.OrderBy(x => x.X));

            //find minimum difference between x blocks
            int dif = int.MaxValue;

            for (int i = 1; i < sortedBlocks.Count; i++)
            {
                if ((sortedBlocks[i].X - sortedBlocks[i - 1].X) != 0)
                {
                    if ((sortedBlocks[i].X - sortedBlocks[i - 1].X) < dif)
                        dif = sortedBlocks[i].X - sortedBlocks[i - 1].X;

                }
            }

            //just to be sure, this time find minimum difference between y blocks
            sortedBlocks = new List<Block>(Blocks.OrderBy(x => x.Y));
            for (int i = 1; i < sortedBlocks.Count; i++)
            {
                if ((sortedBlocks[i].Y - sortedBlocks[i - 1].Y) != 0)
                {
                    if ((sortedBlocks[i].Y - sortedBlocks[i - 1].Y) < dif)
                        dif = sortedBlocks[i].Y - sortedBlocks[i - 1].Y;
                }
            }
            Increase = dif;
        }
    }


    /// <summary>
    /// Generates minecraft function in from of list of strings, each element of list is one line of setblock command
    /// </summary>
    class GenerateMinecraftFuntion
    {
        public List<string> MinecraftFunction { get; }

        public GenerateMinecraftFuntion(List<Block> TranslatedBlocks, int _x, int _z, int _y)
        {
            MinecraftFunction = new List<string>();

            foreach (var item in TranslatedBlocks)
            {
                string singleCommand = "setblock " + Convert.ToString(item.X + _x) + " " + Convert.ToString(item.Z + _z) + " " + Convert.ToString(item.Y + _y) + " " + item.Type + " replace";
                MinecraftFunction.Add(singleCommand);
            }
        }
    }

    /// <summary>
    /// No need to explain
    /// </summary>
    static class SaveMinecraftFuntionToFile
    {
        public static void Save(List<string> MinecraftFunction, string _path)
        {
            File.WriteAllLines(_path, MinecraftFunction);
        }
    }
}


public class ImprovedWebClient : WebClient
{
    CookieContainer cookies = new CookieContainer();
    //^here are automatically stored the cookies

    protected override WebRequest GetWebRequest(Uri address)
    {
        WebRequest request = base.GetWebRequest(address);

        if (request is HttpWebRequest)  //if it is a Http request
            ((HttpWebRequest)request).CookieContainer = cookies;
        //^we bind that cookie container to the request

        return request; // return the modified request (the one with cookies)
    }
}