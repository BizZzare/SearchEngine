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
    public class CommonSearchService : ISearchService
    {
        public async Task<IEnumerable<Result>> LookupAsync(Query request, SearchSites links)
        {
            HtmlWeb website = new HtmlWeb();
            website.AutoDetectEncoding = true;
            website.OverrideEncoding = Encoding.UTF8;

            var tasks = new List<Task<HtmlDocument>>();
            tasks.Add(website.LoadFromWebAsync(links.GoogleUri + request.Body));
            tasks.Add(website.LoadFromWebAsync(links.BingUri + request.Body));
            //tasks.Add(website.LoadFromWebAsync(links.YandexUri + request.Body));
            
            var responce = await Task.WhenAny(tasks).ConfigureAwait(false);

            HtmlDocument doc = responce.Result;

            List<Result> result = null;
           
            if (responce.Id == tasks[0].Id)
                result = Parser.ParseGoogleSearchResult(doc);
            else if (responce.Id == tasks[1].Id)
                result = Parser.ParseBingSearchResult(doc);
            else if (responce.Id == tasks[2].Id)
                result = Parser.ParseYandexSearchResult(doc);

            return result;
        }
    }
}
