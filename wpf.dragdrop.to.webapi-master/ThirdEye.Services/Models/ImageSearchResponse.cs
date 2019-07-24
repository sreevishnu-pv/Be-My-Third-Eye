using System.Collections.Generic;

namespace ThirdEye.Services.Models
{
    public class ImageSearchResponse
    {
        public string SearchQuery { get; set; }
        public List<string> WebSites { set; get; }
        public List<string> Tags { set; get; }
        public string Location { get; set; }
    }
}
