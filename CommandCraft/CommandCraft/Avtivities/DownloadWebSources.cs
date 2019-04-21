using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.Utils;

namespace CommandCraft.Avtivities
{
    class DownloadWebSources
    {
        //inputs
        string htmlMainUrl;
        string htmlDictUrl;


        //outputs
        String htmlDict;
        String htmlMain;
        String jsUrl;
        String jsContent;


        public void SetInput( string _htmlMainUrl, string _htmlDictUrl)
        {
            htmlMainUrl = _htmlMainUrl;
            htmlDictUrl = _htmlDictUrl;
        }

        public void SetOutput(String _htmlDict, String _htmlMain, String _jsUrl, String _jsContent)
        {
            htmlDict = _htmlDict;
            htmlMain = _htmlMain;
            jsUrl = _jsUrl;
            jsContent = _jsContent;
        }

        public void Process()
        {
            using (var site = new ImprovedWebClient())
            {
                site.Headers[HttpRequestHeader.UserAgent] = "Mozilla /5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                htmlMain = site.DownloadString(htmlMainUrl);

                jsUrl = RegexConfig.JsLink.Match(htmlMain).Value;

                site.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";   
                jsContent = site.DownloadString(jsUrl);
                htmlDict = site.DownloadString(htmlDictUrl);
            }
        }

    }
}

