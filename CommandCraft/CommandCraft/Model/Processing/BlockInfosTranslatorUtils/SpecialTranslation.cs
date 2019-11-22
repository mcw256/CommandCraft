using CommandCraft.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft.Model.Processing.BlockInfosTranslatorUtils
{
    static class SpecialTranslation
    {
        public static readonly List<string> SpecialStrings = new List<string>
        {
           "Torch"
        };
        public static readonly List<string> IgnoredAttributes = new List<string>
        {
           "On The Floor",
           "Normal",
           "Inactive",
           "Unactive"
        };

        public static bool IsSpecialTranslationNeeded(string info)
        {
            foreach (var item in SpecialStrings)
            {
                if (info.Contains(item))
                    return true;
            }
            return false;
        }

    }
}
