using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using HtmlAgilityPack;

namespace Grabcraft_Helper.Model.Extractors
{
    class ExtractLayermapURL : Extractor<string, HtmlDocument>
    {
        public override string Output { get; protected set; }

        public override Response Extract(HtmlDocument htmlDoc)
        {
            try
            {
                HtmlNode tabcontentsNode = htmlDoc.DocumentNode.Descendants("div").Where(x => x.Id == "tab-contents").First();
                HtmlNode scriptNode = tabcontentsNode.Descendants("script")
                                                     .Where(x => x.GetAttributeValue("src", "").EndsWith(".js") && x.GetAttributeValue("src", "").Contains("LayerMap"))
                                                     .First();

                Output = scriptNode.GetAttributeValue("src", "");
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }
            return new Response(false, "");
        }
    }
}
