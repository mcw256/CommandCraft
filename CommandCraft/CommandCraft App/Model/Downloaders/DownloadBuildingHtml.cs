using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommandCraft_App.Model.DataTypes;
using CommandCraft_App.Model.Downloaders.Utils;
using HtmlAgilityPack;

namespace CommandCraft_App.Model.Downloaders
{
    class DownloadBuildingHtml : Downloader<string, string>
    {
        public override string Output { get; protected set; }

        public override Response Download(string url)
        {
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDoc = htmlWeb.Load(url);

                HtmlNode tabcontentsNode = htmlDoc.DocumentNode.Descendants("div").Where(x => x.Id == "tab-contents").First();
                HtmlNode scriptNode = tabcontentsNode.Descendants("script")
                                                     .Where(x => x.GetAttributeValue("src", "").EndsWith(".js") && x.GetAttributeValue("src", "").Contains("LayerMap"))
                                                     .First();

                Output = scriptNode.GetAttributeValue("src", "");
            }
            catch (WebException)
            {
                return new Response(true, "Connection error");
            }
            catch(Exception)
            {
                return new Response(true, "Error");
            }

            return new Response(false, "");
        }
    }
}
