using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandCraft.Model.DataTypes;

namespace CommandCraft.Model.Processing.BlockInfosTranslatorUtils
{
    static class Utils
    {
        public static void RemoveIgnoredAttributes(List<string> attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (SpecialTranslation.IgnoredAttributes.Contains(attributes[i]))
                    attributes.RemoveAt(i);
            }
        }

        public static string RemovePrecedingSpace(string info)
        {
            return Regex.Replace(info, @"^\s", "");
        }

        public static void TranslateName( BlockMInfo blockMInfo, string name)
        {
            var dic = new SearchNamesDictionary();

            if (dic.Search(name) == false)
                blockMInfo.IsMismatched = true;

            else
                blockMInfo.Name = dic.Output;
        }

        public static bool TranslateInfo(BlockMInfo blockMInfo, string info)
        {
            var dic = new SearchInfosDictionary();

            if (dic.Search(info) == false)
                return false;

           
                blockMInfo.Name = dic.Output;
                blockMInfo.Attributes = new List<string>();
            return true;

        }

        public static void TranslateAttributes( BlockMInfo blockMInfo, List<string> attriubtes )
        {
            var dic = new SearchAttributesDictionary();
            var translatedAttributes = new List<string>();

            foreach (var item in attriubtes)
            {
                if (dic.Search(item) == false)
                {
                    blockMInfo.IsMismatched = true;
                    return;
                }
                else
                    translatedAttributes.Add(dic.Output);
            }
            blockMInfo.Attributes = translatedAttributes;
        }
    }
}
