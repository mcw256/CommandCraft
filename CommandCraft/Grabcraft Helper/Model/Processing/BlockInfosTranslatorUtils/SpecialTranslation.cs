using Grabcraft_Helper.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model.Processing.BlockInfosTranslatorUtils
{
    static class SpecialTranslation
    {
        public static readonly List<string> specialStrings = new List<string>
        {
           "Torch"
        };
        public static readonly List<string> ignoredAttributes = new List<string>
        {
           "On The Floor",
           "Normal",
           "Inactive",
           "Unactive"
        };

        public static bool IsSpecialTranslationNeeded(string info)
        {
            foreach (var item in specialStrings)
            {
                if (info.Contains(item))
                    return true;
            }
            return false;
        }

    }
}
