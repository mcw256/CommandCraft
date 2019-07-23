using CommandCraft_App.Model.DataTypes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCraft_App.Model.Extractors
{
    class ExtractBuildingName
    {
        public string Output { get; protected set; }

        public Response Extract(HtmlDocument htmlDoc)
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
