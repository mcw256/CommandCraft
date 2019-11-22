using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;
using CommandCraft.Model.DataTypes;
using CommandCraft.Model.Downloaders;
using CommandCraft.Model.Extractors;
using CommandCraft.Model.FileOperations.Loaders;
using CommandCraft.Model.FileOperations.Savers;
using CommandCraft.Model.Processing;
using CommandCraft.Models.FileOperations.Savers;

namespace CommandCraft.Model
{
    /// <summary>
    /// Main class to coordinate everything. I don't know how will I glue it with ViewModel and Program.cs, but I will figure out.
    /// </summary>
    static class ActionManager
    {
        static DataContainer _data = new DataContainer();
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
               var loadPlayerSavesList = new LoadPlayerSavesList();
               IsSaveToMinecraftAvailable = true;

               // - - - - - - - - - - - - -

               //load block names dictionary
               response = loadBlockNamesDictionary.Load(); //get resposnse
               if (response.IsError) return response;
               _data.LoadBlockNamesDictionaryOutput = loadBlockNamesDictionary.Output;

               //load block attributes dictionary
               response = loadBlockAttributesDictionary.Load();
               if (response.IsError) return response;
               _data.LoadBlockAttributesDictionaryOutput = loadBlockAttributesDictionary.Output;

               //load user block infos dictionary
               response = loadUserDefinedBlockInfosDictionary.Load();
               if (response.IsError) return response;
               _data.LoadUserDefinedBlockInfosDictionary = loadUserDefinedBlockInfosDictionary.Output;

               //load user config
               response = loadUserConfig.Load();
               if (response.IsError)
               {
                   SaveUserConfig(HowToHandleMismatch.Ignore, "", false);
                   response = loadUserConfig.Load();
                   if (response.IsError)
                       return response;
               }
               _data.LoadUserConfigOutput = loadUserConfig.Output;

               //load player saves list
               response = loadPlayerSavesList.Load(_data.LoadUserConfigOutput.MinecraftPath);
               if (response.IsError) return response;
               _data.LoadPlayerSavesListOutput = loadPlayerSavesList.Output;

               return new Response(false, "");
           });
        }

        public static async Task<Response> DownloadAndProcessBuilding(string buildingUrl)
        {
            return await Task.Run(() =>
            {
                //check whether the dictionaries has been set
                if (_data.LoadBlockAttributesDictionaryOutput == null) return new Response(true, "Attributes dictionary not set");
                if (_data.LoadBlockNamesDictionaryOutput == null) return new Response(true, "Names dictionary not set");

                Response response;

                //initialize action classes
                var downloadBuildingHtml = new DownloadBuildingHtml();
                var extractBuildingName = new ExtractBuildingName();
                var extractLayermapUrl = new ExtractLayermapUrl();
                var downloadLayermap = new DownloadLayermap();
                var layermapDeserializer = new LayermapDeserializer();
                var coordsNormalizer = new CoordsNormalizer();
                var blockInfosTranslator = new BlockInfosTranslator(_data.LoadBlockNamesDictionaryOutput, _data.LoadBlockAttributesDictionaryOutput, _data.LoadUserDefinedBlockInfosDictionary);

                //download building html, using passed building url
                response = downloadBuildingHtml.Download(buildingUrl);
                if (response.IsError) return response;
                _data.DownloadBuildingHtmlOutput = downloadBuildingHtml.Output;

                //extract building name from /\ html output
                response = extractBuildingName.Extract(_data.DownloadBuildingHtmlOutput);
                if (response.IsError) return response;
                _data.ExtractBuildingNameOutput = extractBuildingName.Output;

                //extract layermapUrl from //\\ html output
                response = extractLayermapUrl.Extract(_data.DownloadBuildingHtmlOutput);
                if (response.IsError) return response;
                _data.ExtractLayermapUrlOutput = extractLayermapUrl.Output;

                //download layermap from /\ layermapUrl 
                response = downloadLayermap.Download(_data.ExtractLayermapUrlOutput);
                if (response.IsError) return response;
                _data.DownloadLayermapOutput = downloadLayermap.Output;

                //deserialize /\ layermap
                response = layermapDeserializer.Process(_data.DownloadLayermapOutput);
                if (response.IsError) return response;
                _data.LayermapDeserializerOutput = layermapDeserializer.Output;

                //translate coords from GBlocks(this /\ deserialzed layermap)
                response = coordsNormalizer.Process(_data.LayermapDeserializerOutput.GBlocks.Select(x => x.Coords).ToList<Coords>(), _data.LayermapDeserializerOutput.Interval);
                if (response.IsError) return response;
                _data.CoordsNormalizerOutput = coordsNormalizer.Output;

                //translate GInfos from GBlocks( //\\ deserialzed layermap)
                response = blockInfosTranslator.Process(_data.LayermapDeserializerOutput.GBlocks.Select(x => x.Info).ToList());
                if (response.IsError) return response;
                _data.BlockInfosTranslatorOutput = blockInfosTranslator.Output;


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

                response = mBlocksGluer.Process(_data.CoordsNormalizerOutput, _data.BlockInfosTranslatorOutput);
                if (response.IsError) return response;
                _data.MBlocksGluerOutput = mBlocksGluer.Output;

                response = mFunctionComposer.Process(_data.MBlocksGluerOutput, howToHandleMismatch, _data.ExtractBuildingNameOutput);
                if (response.IsError) return response;
                _data.MFunctionComposerOutput = mFunctionComposer.Output;

                response = saveMFunction.Save(_data.MFunctionComposerOutput);
                if (response.IsError) return response;

                if (response.IsError == false && response.ErrorMsg == "user canceled")
                    return response;

                return new Response(false, "");
            });
        }

        public static async Task<Response> AssembleMFunctionAndSaveToMinecraft(HowToHandleMismatch howToHandleMismatch, string saveName)
        {
            return await Task.Run(() =>
            {
                Response response;
                var mBlocksGluer = new MBlocksGluer();
                var mFunctionComposer = new MFunctionComposer();
                var saveMFunctionToMinecraft = new SaveMFunctionToMinecraft();

                response = mBlocksGluer.Process(_data.CoordsNormalizerOutput, _data.BlockInfosTranslatorOutput);
                if (response.IsError) return response;
                _data.MBlocksGluerOutput = mBlocksGluer.Output;

                response = mFunctionComposer.Process(_data.MBlocksGluerOutput, howToHandleMismatch, _data.ExtractBuildingNameOutput);
                if (response.IsError) return response;
                _data.MFunctionComposerOutput = mFunctionComposer.Output;

                response = saveMFunctionToMinecraft.Save(_data.MFunctionComposerOutput, _data.LoadUserConfigOutput.MinecraftPath, saveName);
                if (response.IsError) return response;

                return new Response(false, "");
            });
        }

        public static void SaveUserConfig(HowToHandleMismatch defaultAlternative, string defaultGameSave, bool isMinecraftPathSaved = true)
        {
            var saveUserConfig = new SaveUserConfig();
            if (isMinecraftPathSaved)
                saveUserConfig.Save(new UserConfig(_data.LoadUserConfigOutput.MinecraftPath, defaultGameSave, defaultAlternative));
            else
                saveUserConfig.Save(new UserConfig($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\.minecraft", defaultGameSave, defaultAlternative));
        }

        public static bool AreThereMismatches
        {
            get
            {
                if (_data.BlockInfosTranslatorOutput == null) return false;

                if (_data.BlockInfosTranslatorOutput.Where(x => x.IsMismatched == true).ToList().Count >= 1)
                    return true;
                else
                    return false;
            }
        }

        public static List<string> MismatchesList
        {
            get
            {
                return _data.BlockInfosTranslatorOutput.Where(x => x.IsMismatched == true).Select(x => x.Info).Distinct().ToList();
            }
        }

        public static List<string> PlayerSavesList => _data.LoadPlayerSavesListOutput;

        public static string DefaultPlayerSave => _data.LoadUserConfigOutput.DefaultGameSave;

        public static HowToHandleMismatch DefaultMismatchOption => _data.LoadUserConfigOutput.DefaultMismatchOption;

        public static string BuildingName => _data.ExtractBuildingNameOutput;

        private static bool _isSaveToMinecraftAvailable;
        public static bool IsSaveToMinecraftAvailable
        {
            get => _isSaveToMinecraftAvailable;
            set => _isSaveToMinecraftAvailable = value;
        }

        public static void ResetData()
        {
            _data = new DataContainer();
        }
    }

    class DataContainer
    {
        public Dictionary<string, string> LoadBlockNamesDictionaryOutput { get; set; }
        public Dictionary<string, string> LoadBlockAttributesDictionaryOutput { get; set; }
        public Dictionary<string, string> LoadUserDefinedBlockInfosDictionary { get; set; }
        public UserConfig LoadUserConfigOutput { get; set; }
        public List<string> LoadPlayerSavesListOutput { get; set; }
        public HtmlDocument DownloadBuildingHtmlOutput { get; set; }
        public string ExtractBuildingNameOutput { get; set; }
        public string ExtractLayermapUrlOutput { get; set; }
        public string DownloadLayermapOutput { get; set; }
        public DeserializedLayermap LayermapDeserializerOutput { get; set; }
        public List<Coords> CoordsNormalizerOutput { get; set; }
        public List<BlockMInfo> BlockInfosTranslatorOutput { get; set; }
        public List<MBlock> MBlocksGluerOutput { get; set; }
        public MFunction MFunctionComposerOutput { get; set; }
    }

}
