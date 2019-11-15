using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Grabcraft_Helper.DataTypes;
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
                //HtmlWeb htmlWeb = new HtmlWeb();
                //Output = htmlWeb.Load(url);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                using (var site = new ImprovedWebClient())
                {
                    site.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                    string rawHtml = site.DownloadString(url);
                    Output.LoadHtml(rawHtml);
                }


            }
            catch (WebException e)
            {
                string x = e.Message;
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
