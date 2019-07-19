﻿using System;
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
    class DownloadLayermap : Downloader<string, string>
    {
        public override string Output { get; protected set; }

        public override Response Download(string url)
        {
            try
            {
                using (var site = new ImprovedWebClient())
                {
                    site.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                    Output = site.DownloadString(url);
                }
            }
            catch (WebException)
            {
                return new Response(true, "Connection error");
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }

            return new Response(false, "");
        }
    }
}
