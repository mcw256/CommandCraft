﻿using CommandCraft.Model.DataTypes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;

namespace CommandCraft.Model.Extractors
{
    class ExtractBuildingName : Extractor<string, HtmlDocument>
    {
        public override string Output { get; protected set; }

        public override Response Extract(HtmlDocument htmlDoc)
        {
            try
            {
                Output = htmlDoc.DocumentNode.Descendants("h1").Where(x => x.Id == "content-title").First().InnerText;
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }
            return new Response(false, "");
        }
    }
}
