using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using ThirdEye.Services.Models;

namespace ThirdEye.Services.Services
{
    public class BingWebSearchService
    {
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

        public BingWebSearchResult GetWebSearchResults(string searchQuery)
        {
            var response = new BingWebSearchResult();
            int count = 0;
            int maxLinksToTake = 5;
            var searchResult = BingWebSearch(searchQuery);
            BingCustomSearchResponse bingCustomSearchResponse = JsonConvert.DeserializeObject<BingCustomSearchResponse>(searchResult.jsonResult);

            foreach (var pageValue in bingCustomSearchResponse.webPages.value)
            {
                if (pageValue.url.Contains("youtube"))
                    response.RelatedVideos.Add(pageValue.url);
                else if (count <= maxLinksToTake)
                {
                    response.RelatedLinks.Add(pageValue.url);
                    response.Snippets.Add(pageValue.snippet);
                    count++;
                }
            }

            return response;
        }

        private SearchResult BingWebSearch(string searchQuery)
        {
            var subscriptionKey = ConfigurationManager.AppSettings["BingWebSearchSubscriptionKey"];
            // Construct the search request URI.
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery);

            // Perform request and get a response.
            WebRequest request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = subscriptionKey;
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
}
