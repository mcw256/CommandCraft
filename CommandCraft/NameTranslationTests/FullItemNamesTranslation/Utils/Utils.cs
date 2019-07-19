using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameTranslationTests.FullItemNamesTranslation.Utils
{
    static class Utils
    {
        public static bool AreThereAnyAttributes(string block)
        {
            if (Regex.IsMatch(block, @".+\(.+\)")) return true;
            else return false;

        }

        public static string CutAtributes(string block)
        {
            return Regex.Replace(block, @"\s\(.+\)", "");
        }

        public static List<string> ExtractAttributesList(string block)
        {
            List<string> result = new List<string>();

            foreach (var item in Regex.Matches(block, @"[\w\s-/]+[,\)]"))
            {
                string helper = item.ToString();
                helper = Regex.Replace(helper, @"^\s", "");
                helper = Regex.Replace(helper, @"[,\)]$", "");

                result.Add(helper);

            }
            return result;
        }
    }
}
