﻿using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing.BlockInfosTranslatorUtils
{
    static class Utils
    {
        public static void RemoveIgnoredAttributes(List<string> attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (SpecialTranslation.ignoredAttributes.Contains(attributes[i]))
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