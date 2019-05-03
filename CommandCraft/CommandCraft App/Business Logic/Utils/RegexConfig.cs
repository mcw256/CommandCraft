using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCraft.Business_Logic.Utils
{
    public static class RegexConfig
    {
        public static Regex JsLink { get; } = new Regex(@"https.+\/LayerMap\/.+\.js");

        public static Regex SelectPlane { get; } = new Regex("\"\\d+\":\\[[{}\"\\w:,()-\\\\\\s]+\\]");
        public static Regex SelectBlock { get; } = new Regex(@"{[""\w:,\s()-]+}");
        public static Regex SelectX { get; } = new Regex(@"""x"":\d+");
        public static Regex SelectY { get; } = new Regex(@"""y"":\d+");
        public static Regex SelectType { get; } = new Regex(@"""h"":""[\w\s()-,]+""");

        public static Regex FindKeyAndValue { get; } = new Regex(@">[\w\s]+<\/a><br>\(<em>minecraft:[(<wbr>\w_]+<\/em>"); // >Acacia Boat</a><br>(<em>minecraft:<wbr>acacia_<wbr>boat</em>
        public static Regex FindKey { get; } = new Regex(@">[\w\s]+<\/a>"); //>Acacia Boat</a>
        public static Regex FindValue { get; } = new Regex(@"<em>minecraft:<wbr>[\w(<wbr>)]+<\/em>"); //<em>minecraft:<wbr>acacia_<wbr>boat</em>

    }
}
