using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.Business_Logic.Utils;
using CommandCraft.Business_Logic.DataTypes;

namespace CommandCraft.Business_Logic.Activities
{
    /// <summary>
    /// Downloads html srouce from given urls, also finds jsurl in html srouce and downloads it
    /// </summary>
    class downloadWebSources
    {
        //inputs
        MyString htmlMainUrl;
        MyString htmlDictUrl;


        //outputs
        MyString htmlDict;
        MyString htmlMain;
        MyString jsUrl;
        MyString jsContent;


        public void SetInput( string _htmlMainUrl, string _htmlDictUrl)
        {
            htmlMainUrl = new MyString(_htmlMainUrl);
            htmlDictUrl = new MyString(_htmlDictUrl);
        }

        public void SetOutput(MyString _htmlDict, MyString _htmlMain, MyString _jsUrl, MyString _jsContent)
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
                htmlMain.Value = site.DownloadString(htmlMainUrl.Value);

                jsUrl.Value = RegexConfig.JsLink.Match(htmlMain.Value).Value;

                site.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";   
                jsContent.Value = site.DownloadString(jsUrl.Value);
                htmlDict.Value = site.DownloadString(htmlDictUrl.Value);
            }
        }

    }
}

