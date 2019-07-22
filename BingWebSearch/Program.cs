using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BingWebSearch
{
    class Program
    {

        const string accessKey = "d3f6aca7db514813b877d40fecb5618e";

        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";
        const string searchTerm = "Sri Durga Malleswara Swamy Varla Devasthanam Festival Dance Carnival";

        static void Main(string[] args)
        {
            BingWebSearchResult webSearchResult = new BingWebSearchResult();
            int count = 0;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (accessKey.Length == 32)
            {
                Console.WriteLine("Searching the Web for: " + searchTerm);
                SearchResult result = BingWebSearch(searchTerm);
                Console.WriteLine("\nRelevant HTTP Headers:\n");
                foreach (var header in result.relevantHeaders)
                    Console.WriteLine(header.Key + ": " + header.Value);

                dynamic obj = JObject.Parse(result.jsonResult);
                dynamic jsonResponse = JsonConvert.DeserializeObject(result.jsonResult);

                BingCustomSearchResponse response = JsonConvert.DeserializeObject<BingCustomSearchResponse>(result.jsonResult);

                foreach (var pageValue in response.webPages.value)
                {
                    if (pageValue.url.Contains("youtube"))
                        webSearchResult.RelatedVideos.Add(pageValue.url);
                    else if(count <= 5)
                    {
                        webSearchResult.RelatedLinks.Add(pageValue.url);
                        webSearchResult.Snippets.Add(pageValue.snippet);
                        count++;
                    }

                }
            }
            else
            {
                Console.WriteLine("Invalid Bing Search API subscription key!");
                Console.WriteLine("Please paste yours into the source code.");
            }
            Console.Write("\nPress Enter to exit ");
            Console.ReadLine();
        }

        struct SearchResult
        {
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

        /// <summary>
        /// Makes a request to the Bing Web Search API and returns data as a SearchResult.
        /// </summary>
        static SearchResult BingWebSearch(string searchQuery)
        {
            // Construct the search request URI.
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery);

            // Perform request and get a response.
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Create a result object.
            var searchResult = new SearchResult()
            {
                jsonResult = json,
                relevantHeaders = new Dictionary<String, String>()
            };

            // Extract Bing HTTP headers.
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }
            return searchResult;
        }

    }

    public class BingWebSearchResult
    {
        public BingWebSearchResult()
        {
            RelatedLinks = new List<string>();
            RelatedVideos = new List<string>();
            Snippets = new List<string>();
        }
        public List<string> RelatedLinks { get; set; }
        public List<string> RelatedVideos { get; set; }
        public List<string> Snippets { get; set; }
    }

    public class BingCustomSearchResponse
    {
        public string _type { get; set; }
        public WebPages webPages { get; set; }
    }

    public class WebPages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public WebPage[] value { get; set; }
    }

    public class WebPage
    {
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public string cachedPageUrl { get; set; }
        public OpenGraphImage openGraphImage { get; set; }
    }

    public class OpenGraphImage
    {
        public string contentUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

}
