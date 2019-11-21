using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using Grabcraft_Helper.Model.Processing.BlockInfosTranslatorUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing
{
    class BlockInfosTranslator : Processor<List<BlockMInfo>, List<BlockGInfo>>
    {
        public override List<BlockMInfo> Output { get; protected set; } = new List<BlockMInfo>();
        public BlockInfosTranslator(Dictionary<string, string> namesDictionary, Dictionary<string, string> attributesDictionary, Dictionary<string, string> userDictionary)
        {
            SearchNamesDictionary.NamesDictionary = namesDictionary;
            SearchAttributesDictionary.AttributesDictionary = attributesDictionary;
            SearchInfosDictionary.InfosDictionary = userDictionary;
        }

        public override Response Process(List<BlockGInfo> blockGInfos)
        {
            try
            {
                foreach (var item in blockGInfos)
                {
                    BlockMInfo mInfo = new BlockMInfo("", new List<string>());

                    item.Info = Utils.RemovePrecedingSpace(item.Info);
                    string nontranslatedInfo = item.Info;

                    if(Utils.TranslateInfo(mInfo, item.Info))
                    {
                        Output.Add(mInfo);
                        continue;
                    }


                    if (item.IsValid && item.HasAttributes)
                        Utils.RemoveIgnoredAttributes(item.Attributes);

                    if (SpecialTranslation.IsSpecialTranslationNeeded(item.Info) || item.IsValid == false)
                    {
                        Output.Add(TranslateExtraordinaryBlocks.Translate(item.Info));
                        continue;
                    }

                    
                    Utils.TranslateName(mInfo, item.Name);
                    if (item.HasAttributes) Utils.TranslateAttributes(mInfo, item.Attributes);

                    if (mInfo.IsMismatched)
                    {
                        mInfo.Name = nontranslatedInfo;
                        mInfo.Attributes = new List<string>();
                    }
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
