using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using Grabcraft_Helper.Model.Downloaders;
using Grabcraft_Helper.Model.Extractors;
using Grabcraft_Helper.Model.FileOperations.Loaders;
using Grabcraft_Helper.Model.FileOperations.Savers;
using Grabcraft_Helper.Model.Processing;
using Grabcraft_Helper.Models.FileOperations.Savers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model
{
    /// <summary>
    /// Main class to coordinate everything. I don't know how will I glue it with ViewModel and Program.cs, but I will figure out.
    /// </summary>
    static class ActionManager
    {
        static DataContainer data = new DataContainer();

        /// <summary>
        /// Loads necessary dictionaries
        /// </summary>
        /// <returns></returns>
        public static async Task<Response> LoadDictionaries()
        {
            return await Task.Run(() =>
           {
               Response response;
               //initialize actions classes
               var loadBlockNamesDictionary = new LoadBlockNamesDictionary();
               var loadBlockAttributesDictionary = new LoadBlockAttributesDictionary();
               var loadUserDefinedBlockInfosDictionary = new LoadUserDefinedBlockInfosDictionary();
               var loadUserConfig = new LoadUserConfig();

               // - - - - - - - - - - - - -

               //load block names dictionary
               response = loadBlockNamesDictionary.Load(); //get resposnse
               if (response.IsError) return response;
               data.LoadBlockNamesDictionaryOutput = loadBlockNamesDictionary.Output;

               //load block attributes dictionary
               response = loadBlockAttributesDictionary.Load();
               if (response.IsError) return response;
               data.LoadBlockAttributesDictionaryOutput = loadBlockAttributesDictionary.Output;

               //load user block infos dictionary
               response = loadUserDefinedBlockInfosDictionary.Load();
               if (response.IsError) return response;
               data.LoadUserDefinedBlockInfosDictionary = loadUserDefinedBlockInfosDictionary.Output;

               //load user config
               response = loadUserConfig.Load();
               if (response.IsError) return response;
               data.LoadUserConfigOutput = loadUserConfig.Output;

               return new Response(false, "");
           });
        }

        /// <summary>
        /// Downloads building data and proccesses(translate) it using dictionaries
        /// </summary>
        /// <param name="buildingURL"></param>
        /// <returns></returns>
        public static async Task<Response> DownloadAndProcessBuilding(string buildingURL)
        {
            return await Task.Run(() =>
            {
                //check whether the dictionaries has been set
                if (data.LoadBlockAttributesDictionaryOutput == null) return new Response(true, "Attributes dictionary not set");
                if (data.LoadBlockNamesDictionaryOutput == null) return new Response(true, "Names dictionary not set");
                
                Response response;

                //initialize action classes
                var downloadBuildingHtml = new DownloadBuildingHtml();
                var extractBuildingName = new ExtractBuildingName();
                var extractLayermapUrl = new ExtractLayermapURL();
                var downloadLayermap = new DownloadLayermap();
                var layermapDeserializer = new LayermapDeserializer();
                var coordsNormalizer = new CoordsNormalizer();
                var blockInfosTranslator = new BlockInfosTranslator(data.LoadBlockNamesDictionaryOutput, data.LoadBlockAttributesDictionaryOutput);

                //download building html, using passed building url
                response = downloadBuildingHtml.Download(buildingURL);
                if (response.IsError) return response;
                data.DownloadBuildingHtmlOutput = downloadBuildingHtml.Output;

                //extract building name from /\ html output
                response = extractBuildingName.Extract(data.DownloadBuildingHtmlOutput);
                if (response.IsError) return response;
                data.ExtractBuildingNameOutput = extractBuildingName.Output;

                //extract layermapUrl from //\\ html output
                response = extractLayermapUrl.Extract(data.DownloadBuildingHtmlOutput);
                if (response.IsError) return response;
                data.ExtractLayermapURLOutput = extractLayermapUrl.Output;

                //download layermap from /\ layermapUrl 
                response = downloadLayermap.Download(data.ExtractLayermapURLOutput);
                if (response.IsError) return response;
                data.DownloadLayermapOutput = downloadLayermap.Output;

                //deserialize /\ layermap
                response = layermapDeserializer.Process(data.DownloadLayermapOutput);
                if (response.IsError) return response;
                data.LayermapDeserializerOutput = layermapDeserializer.Output;

                //translate coords from GBlocks(this /\ deserialzed layermap)
                response = coordsNormalizer.Process(data.LayermapDeserializerOutput.GBlocks.Select(x => x.Coords).ToList<Coords>(), data.LayermapDeserializerOutput.Interval);
                if (response.IsError) return response;
                data.CoordsNormalizerOutput = coordsNormalizer.Output;

                //translate GInfos from GBlocks( //\\ deserialzed layermap)
                response = blockInfosTranslator.Process(data.LayermapDeserializerOutput.GBlocks.Select(x => x.Info).ToList());
                if (response.IsError) return response;
                data.BlockInfosTranslatorOutput = blockInfosTranslator.Output;


                return new Response(false, "");

            });
        }

        public static async Task<Response> AssembleMFunctionAndSave(HowToHandleMismatch howToHandleMismatch)
        {
            return await Task.Run(() =>
            {
                Response response;
                var mBlocksGluer = new MBlocksGluer();
                var mFunctionComposer = new MFunctionComposer();
                var saveMFunction = new SaveMFunction();

                response = mBlocksGluer.Process(data.CoordsNormalizerOutput, data.BlockInfosTranslatorOutput);
                if (response.IsError) return response;
                data.MBlocksGluerOutput = mBlocksGluer.Output;

                response = mFunctionComposer.Process(data.MBlocksGluerOutput, howToHandleMismatch, data.ExtractBuildingNameOutput);
                if (response.IsError) return response;
                data.MFunctionComposerOutput = mFunctionComposer.Output;

                response = saveMFunction.Save(data.MFunctionComposerOutput);
                if (response.IsError) return response;

                if (response.IsError == false && response.ErrorMsg == "user canceled")
                    return response;

                return new Response(false, "");
            });
        }

        public static async Task<Response> AssembleMFunctionAndSaveToMinecraft(HowToHandleMismatch howToHandleMismatch)
        {
            return await Task.Run(() =>
            {
                Response response;
                var mBlocksGluer = new MBlocksGluer();
                var mFunctionComposer = new MFunctionComposer();
                var saveMFunctionToMinecraft = new SaveMFunctionToMinecraft();

                response = mBlocksGluer.Process(data.CoordsNormalizerOutput, data.BlockInfosTranslatorOutput);
                if (response.IsError) return response;
                data.MBlocksGluerOutput = mBlocksGluer.Output;

                response = mFunctionComposer.Process(data.MBlocksGluerOutput, howToHandleMismatch, data.ExtractBuildingNameOutput);
                if (response.IsError) return response;
                data.MFunctionComposerOutput = mFunctionComposer.Output;


                // TODO, user config needs to be implemented for this to work
                //response = saveMFunctionToMinecraft.Save(data.MFunctionComposerOutput, "", "" );
                // if (response.IsError) return response;

                return new Response(false, "");
            });
        }

        public static void SaveUserConfig(HowToHandleMismatch defaultAlternative, string defaultGameSave)
        {
            //TODO
        }

        public static bool AreThereMismatches
        {
            get
            {
                if (data.BlockInfosTranslatorOutput == null) return false;

                if (data.BlockInfosTranslatorOutput.Where(x => x.IsMismatched == true).ToList().Count >= 1)
                    return true;
                else
                    return false;
            }
        }

        public static List<string> MismatchesList
        {
            get
            {
                return data.BlockInfosTranslatorOutput.Where(x => x.IsMismatched == true).Select(x => x.Info).Distinct().ToList();
            }
        }

        public static string BuildingName
        {
            get { return data.ExtractBuildingNameOutput; }
        }

        public static void ResetData()
        {
            data = new DataContainer();
        }
    }

    class DataContainer
    {

        public Dictionary<string, string> LoadBlockNamesDictionaryOutput { get; set; }
        public Dictionary<string, string> LoadBlockAttributesDictionaryOutput { get; set; }
        public Dictionary<string, string> LoadUserDefinedBlockInfosDictionary { get; set; }
        public UserConfig LoadUserConfigOutput { get; set; }
        public HtmlDocument DownloadBuildingHtmlOutput { get; set; }
        public string ExtractBuildingNameOutput { get; set; }
        public string ExtractLayermapURLOutput { get; set; }
        public string DownloadLayermapOutput { get; set; }
        public DeserializedLayermap LayermapDeserializerOutput { get; set; }
        public List<Coords> CoordsNormalizerOutput { get; set; }
        public List<BlockMInfo> BlockInfosTranslatorOutput { get; set; }
        public List<MBlock> MBlocksGluerOutput { get; set; }
        public MFunction MFunctionComposerOutput { get; set; }
    }

}
