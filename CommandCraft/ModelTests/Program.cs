using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft_App.Model.DataTypes;
using CommandCraft_App.Model.Downloaders;
using CommandCraft_App.Model.Extractors;
using CommandCraft_App.Model.FileOperations.Loaders;
using CommandCraft_App.Model.Processing;

namespace ModelTests
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Download Html
            var downloadHtml = new DownloadBuildingHtml();
            string downloadHtmlInput = @"https://www.grabcraft.com/minecraft/classy-city-park/parks";
            
            Response response = downloadHtml.Download(downloadHtmlInput);

            Console.WriteLine($"{downloadHtml.ToString()} | {response.IsError} | {response.ErrorMsg}");

            var downloadHtmlOutput = downloadHtml.Output;

            #endregion
            //--------------------------------------------------

            #region Extract Building Name
            var extractBuildingName = new ExtractBuildingName();
            var extractBuildingNameInput = downloadHtmlOutput;

            response = extractBuildingName.Extract(extractBuildingNameInput);

            Console.WriteLine($"{extractBuildingName.ToString()} | {response.IsError} | {response.ErrorMsg}");

            var extractBuildingNameOutput = extractBuildingName.Output;

            Console.WriteLine(extractBuildingNameOutput);

            #endregion
            //--------------------------------------------------

            #region Extract Layermap URL
            var extractLayermapUrl = new ExtractLayermapURL();
            var extractLayermapUrlInput = downloadHtmlOutput;

            response = extractLayermapUrl.Extract(extractLayermapUrlInput);

            Console.WriteLine($"{extractLayermapUrl.ToString()} | {response.IsError} | {response.ErrorMsg}");

            var extractLayermapUrlOutput = extractLayermapUrl.Output;


            Console.WriteLine(extractLayermapUrlOutput);

            #endregion
            //--------------------------------------------------

            #region Download Layermap
            var downloadLayermap = new DownloadLayermap();
            var downloadLayermapInput = extractLayermapUrlOutput;

            response = downloadLayermap.Download(downloadLayermapInput);

            Console.WriteLine($"{downloadLayermap.ToString()} | {response.IsError} | {response.ErrorMsg}");

            var downloadLayermapOutput = downloadLayermap.Output;


            //Console.WriteLine(downloadLayermapOutput);
            #endregion

            // -------------------------------------------------

            #region Layermap Deserializer
            var layermapDeserializer = new LayermapDeserializer();

            var layermapDeserializerInput = downloadLayermapOutput;

            response = layermapDeserializer.Process(layermapDeserializerInput);

            Console.WriteLine($"{layermapDeserializer.ToString()} | {response.IsError} | {response.ErrorMsg}");

            //Console.WriteLine($"Interval: {layermapDeserializer.Output.Interval}");
            //foreach (var item in layermapDeserializer.Output.GBlocks)
            //{
            //    Console.WriteLine($"{item.Coords.X},{item.Coords.Y},{item.Coords.Z}");
            //    Console.WriteLine($"{item.Info.Info} | {item.Info.Name} | valid: {item.Info.IsValid} | hasAttributes: {item.Info.HasAttributes}");
            //    if (item.Info.HasAttributes)
            //        foreach (var q in item.Info.Attributes)
            //            Console.WriteLine(q);

            //}
            #endregion

            // -------------------------------------------------

            #region Coords Normalizer

            var coordsNormalizer = new CoordsNormalizer();

            response = coordsNormalizer.Process(layermapDeserializer.Output.GBlocks.Select(x => x.Coords).ToList<Coords>(), layermapDeserializer.Output.Interval);

            Console.WriteLine($"{coordsNormalizer.ToString()} | {response.IsError} | {response.ErrorMsg}");

            //Console.WriteLine($"x:{coordsNormalizer.Output.Max(x=>x.X)}, y:{coordsNormalizer.Output.Max(x => x.Y)}, z:{coordsNormalizer.Output.Max(x => x.Z)}");

            #endregion Coords Normalizer

            // -------------------------------------------------

            #region Load Block Names Dictionary
            var loadBlockNamesDictionary = new LoadBlockNamesDictionary();

            response = loadBlockNamesDictionary.Load();

            Console.WriteLine($"{loadBlockNamesDictionary.ToString()} | {response.IsError} | {response.ErrorMsg}");

            #endregion

            // -------------------------------------------------

            #region Load Block Attributes Dictionary

            var loadBlockAttributesDictionary = new LoadBlockAttributesDictionary();

            response = loadBlockAttributesDictionary.Load();

            Console.WriteLine($"{loadBlockAttributesDictionary.ToString()} | {response.IsError} | {response.ErrorMsg}");

            #endregion

            // -------------------------------------------------

            #region Block Infos Translator

            var blockInfosTranslator = new BlockInfosTranslator(loadBlockNamesDictionary.Output, loadBlockAttributesDictionary.Output);

            response = blockInfosTranslator.Process(layermapDeserializer.Output.GBlocks.Select(x => x.Info).ToList());

            Console.WriteLine($"{blockInfosTranslator.ToString()} | {response.IsError} | {response.ErrorMsg}");

            //foreach (var item in blockInfosTranslator.Output)
            //{
            //    Console.Write($"mismatched: {item.IsMismatched} | info: {item.Name}");
            //    if(item.IsMismatched == false)
            //    {
            //        Console.WriteLine($" | name:{item.Name}");
            //        foreach (var q in item.Attributes)
            //        {
            //            Console.WriteLine($"\t{q}");
            //        }

            //    }
            //    else
            //        Console.WriteLine();

            //    Console.WriteLine("---------------");
            //}

            #endregion

            // -------------------------------------------------

            #region MBlocks Gluer

            var mBlocksGluer = new MBlocksGluer();

            response = mBlocksGluer.Process(coordsNormalizer.Output, blockInfosTranslator.Output);

            Console.WriteLine($"{mBlocksGluer.ToString()} | {response.IsError} | {response.ErrorMsg}");

            #endregion

            // -------------------------------------------------

            #region MFunction Composer

            var mFunctionComposer = new MFunctionComposer();

            response = mFunctionComposer.Process(mBlocksGluer.Output, HowToHandleMismatch.Red_Wool, extractBuildingName.Output);

            Console.WriteLine($"{mFunctionComposer.ToString()} | {response.IsError} | {response.ErrorMsg}");

            foreach (var item in mFunctionComposer.Output.Content)
            {
                Console.WriteLine(item);
            }

            #endregion

            // -------------------------------------------------

            #region Load Block Attributes Dictionary

            //var loadBlockAttributesDictionary = new LoadBlockAttributesDictionary();

            //response = loadBlockAttributesDictionary.Load();

            //Console.WriteLine($"{loadBlockAttributesDictionary.ToString()} | {response.IsError} | {response.ErrorMsg}");

            #endregion

        }



    }
}
