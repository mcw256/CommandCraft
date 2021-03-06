﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;
using CommandCraft.Model.DataTypes;
using HtmlAgilityPack;

namespace CommandCraft.Model.Extractors
{
    class ExtractLayermapUrl : Extractor<string, HtmlDocument>
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
