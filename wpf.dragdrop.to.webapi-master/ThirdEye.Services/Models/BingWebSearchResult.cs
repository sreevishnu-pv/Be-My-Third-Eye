using System.Collections.Generic;

namespace ThirdEye.Services.Models
{
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
}
