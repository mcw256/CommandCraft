using CommandCraft_App.Model.DataTypes;
using CommandCraft_App.Model.Processing.BlockInfosTranslatorUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing
{
    class BlockInfosTranslator : Processor<List<BlockMInfo>, List<BlockGInfo>>
    {
        public override List<BlockMInfo> Output { get; protected set; }
        public BlockInfosTranslator(Dictionary<string, string> _namesDictionary, Dictionary<string, string> _attributesDictionary)
        {
            SearchNamesDictionary.NamesDictionary = _namesDictionary;
            SearchAttributesDictionary.AttributesDictionary = _attributesDictionary;
        }

        public override Response Process(List<BlockGInfo> blockGInfos)
        {
            try
            {
                foreach (var item in blockGInfos)
                {
                    item.Info = Utils.RemovePrecedingSpace(item.Info);

                    if (item.IsValid && item.HasAttributes)
                        Utils.RemoveIgnoredAttributes(item.Attributes);

                    if (SpecialTranslation.IsSpecialTranslationNeeded(item.Info) || item.IsValid == false)
                    {
                        Output.Add(TranslateExtraordinaryBlocks.Translate(item.Info));
                        continue;
                    }

                    BlockMInfo mInfo = new BlockMInfo("", new List<string>());
                    Utils.TranslateName(mInfo, item.Name);
                    if (item.HasAttributes) Utils.TranslateAttributes(mInfo, item.Attributes);

                    Output.Add(mInfo);
                }
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }
            return new Response(false, "");
        }
      
    }
}
