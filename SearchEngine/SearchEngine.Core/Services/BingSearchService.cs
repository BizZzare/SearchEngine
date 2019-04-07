using SearchEngine.Core.Interfaces;
using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SearchEngine.Core.Services
{
    public class BingSearchService
    {
        private string AccessKey { get; set; }
        private string UriBase { get; set; }
        const string searchTerm = "Microsoft Cognitive Services";
        public BingSearchService()
        {
            AccessKey = "e0e9294df414474f86ce9f42cab2b143";
            UriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";
        }
        
        public IEnumerable<Result> Lookup(Query request)
        {
            // Construct the search request URI.
            var uriQuery = UriBase + "?q=" + Uri.EscapeDataString(request.Body);

            // Perform request and get a response.
            WebRequest requestToBing = HttpWebRequest.Create(uriQuery);
            requestToBing.Headers["Ocp-Apim-Subscription-Key"] = AccessKey;
            HttpWebResponse response = (HttpWebResponse)requestToBing.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            dynamic jsonData = JsonConvert.DeserializeObject(json);

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


        private string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            char last = ' ';
            int offset = 0;
            int indentLength = 2;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\\':
                        if (quote && last != '\\') ignore = true;
                        break;
                }

                if (quote)
                {
                    sb.Append(ch);
                    if (last == '\\' && ignore) ignore = false;
                }
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case ']':
                        case '}':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (quote || ch != ' ') sb.Append(ch);
                            break;
                    }
                }
                last = ch;
            }
            return sb.ToString().Trim();
        }

    }
}
