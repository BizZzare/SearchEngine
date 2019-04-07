using HtmlAgilityPack;
using SearchEngine.Core.Interfaces;
using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Core.Services
{
    public class Parser
    {
        public static List<Result> ParseGoogleSearchResult(HtmlDocument doc)
        {
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='g']");

            List<Result> recordList = new List<Result>();
            for (int i = 0; i < nodes.Count && i < 10; i++)
            {
                var title = nodes[i].Descendants("h3").ElementAt(0).InnerText;
                var link = (nodes[i].Descendants("a")?.ElementAt(0)?.Attributes)
                    ?.FirstOrDefault(e => e.Name.Contains("href"))?.Value.Trim("/url?q=".ToCharArray());
                var snpLng = nodes[i].Descendants("span");
                var snippet = (snpLng.Count() > 1) ? snpLng.ElementAt(1).InnerText : "";
                
                recordList.Add(new Result()
                {
                    Title = title,
                    Link = link,
                    Snippet = snippet,
                    Index = i,
                    Source = "Google.com"
                });
            }
            return recordList;
        }

        public static List<Result> ParseBingSearchResult(HtmlDocument doc)
        {
            var nodes = doc.DocumentNode.SelectNodes("//li[@class='b_algo']");

            List<Result> recordList = new List<Result>();
            for (int i = 0; i < nodes.Count && i < 10; i++)
            {
                var title = nodes[i].Descendants("h2").ElementAt(0).InnerText;
                var link = (nodes[i].Descendants("a")?.ElementAt(0)?.Attributes)
                    ?.FirstOrDefault(e => e.Name.Contains("href"))?.Value;
                var snpLng = nodes[i].Descendants("p");
                var snippet = (snpLng.Count() >= 1) ? snpLng.ElementAt(0).InnerText : "";

                recordList.Add(new Result()
                {
                    Title = title,
                    Link = link,
                    Snippet = snippet,
                    Index = i,
                    Source = "Bing.com"
                });
            }
            return recordList;
        }

        public static List<Result> ParseYandexSearchResult(HtmlDocument doc)
        {
            var nodes = doc.DocumentNode.SelectNodes("//li[@class='serp-item']");

            List<Result> recordList = new List<Result>();
            for (int i = 0; i < nodes.Count && i < 10; i++)
            {
                var title = nodes[i].Descendants("h2").ElementAt(0).InnerText;
                var link = (nodes[i].Descendants("a")?.ElementAt(0)?.Attributes)
                    ?.FirstOrDefault(e => e.Name.Contains("href"))?.Value;
                var snpLng = nodes[i].Descendants("div").Where(el =>
                    el.Attributes.Contains("text-container typo typo_text_m typo_line_m organic__text"));
                var snippet = (snpLng.Count() >= 1) ? snpLng.ElementAt(0).InnerText : "";

                recordList.Add(new Result()
                {
                    Title = title,
                    Link = link,
                    Snippet = snippet,
                    Index = i,
                    Source = "Yandex.com"
                });
            }
            return recordList;
        }
    }
}
