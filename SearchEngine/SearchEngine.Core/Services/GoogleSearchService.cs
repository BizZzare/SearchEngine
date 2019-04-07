using SearchEngine.Core.Interfaces;
using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SearchEngine.Core.Services
{
    public class GoogleSearchService
    {
        private string Cx { get; set; }
        private string ApiKey { get; set; }

        public GoogleSearchService()
        {
            Cx = "007570066060128543612:osu7v57pgta";
            ApiKey = "AIzaSyAOX8OpvEDcp91D896L3S5D8Fuhd76VGWk";
        }

        public IEnumerable<Result> Lookup(Query request)
        {
            var requestToGoole = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + ApiKey + "&cx=" + Cx + "&q=" + request.Body);

            var response = (HttpWebResponse)requestToGoole.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseString = reader.ReadToEnd();

            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            var results = new List<Result>();

            foreach (var item in jsonData.items)
            {
                results.Add(new Result
                {
                    Title = item.title,
                    Link = item.link,
                    Snippet = item.snippet,
                    Query = request
                });
            }

            return results;
        }

    }
}
