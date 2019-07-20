using CommandCraft_App.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Processing.BlockInfosTranslatorUtils
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
            if (specialStrings.Contains(info))
                return true;

            return false;
        }

    }
}
