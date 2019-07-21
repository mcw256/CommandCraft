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
    class DownloadBuildingHtml : Downloader<HtmlDocument, string>
    {
        public override HtmlDocument Output { get; protected set; }

        public override Response Download(string url)
        {
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                Output = htmlWeb.Load(url);
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
