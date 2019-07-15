using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace NameTranslationTests
{
    static class DownloadDictionary
    {
        public static Dictionary<string, string> Download()
        {
            throw new Exception("This function has been already used. It's one-off");

            Dictionary<string, string> BlockNamesTranslations = new Dictionary<string, string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(@"https://www.digminecraft.com/lists/item_id_list_pc.php?fbclid=IwAR2xBKrh6ayrSUYrDLZut0IPMUH4VO_QQ0YacUGPDvIMBjiSf5zCZIGR4iA");

            var table = htmlDoc.DocumentNode.Descendants("table").First(x => x.Id == "minecraft_items");

            //var rowCol = table.Descendants("tr").Skip<HtmlNode>(1);
            //foreach (var item in rowCol)
            //{
            //    BlockNamesTranslations.Add(item.Descendants("a").First().InnerText, item.Descendants("em").First().InnerText);
            //}

            //it can also be done this way...
            BlockNamesTranslations = table.Descendants("td")
                                          .Where(x => x.Descendants("em").Any())
                                            .Select(x => new
                                            {
                                                Key = x.Descendants("a").FirstOrDefault()?.InnerText,
                                                Value = x.Descendants("em").FirstOrDefault()?.InnerText
                                            })
                                            .Where(x => !string.IsNullOrWhiteSpace(x.Key) && !string.IsNullOrWhiteSpace(x.Value))
                                            .ToDictionary(x => x.Key, x => x.Value);


            return BlockNamesTranslations;
        }
    }
}
