using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.Model.DataTypes;
using Grabcraft_Helper.Model.Downloaders.Utils;
using HtmlAgilityPack;

namespace Grabcraft_Helper.Model.Downloaders
{
    class DownloadBuildingHtml : Downloader<HtmlDocument, string>
    {
        public override HtmlDocument Output { get; protected set; } = new HtmlDocument();

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
