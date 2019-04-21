using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCraft.Utils
{
    public static class RegexConfig
    {
        public static Regex JsLink { get; } = new Regex(@"https.+\/LayerMap\/.+\.js");

    }
}
