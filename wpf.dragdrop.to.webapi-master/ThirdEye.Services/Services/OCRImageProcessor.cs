using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThirdEye.Services.Services
{
    public class OCRImageProcessor
    {
        static readonly Regex onlyAlphabetRegex = new Regex("^[a-zA-Z]+$");
        static readonly Regex websiteRegex = new Regex("^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)?[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$");
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/ocr";
        private readonly string _subscriptionKey;

        public OCRImageProcessor(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }
        public async Task<OCRSearchResponse> GetOCRText(byte[] imageBytes)
        {
            string contentString = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                string uri = $"{uriBase}?language=unk&detectOrientation=true";

                HttpResponseMessage response;

                using (ByteArrayContent content = new ByteArrayContent(imageBytes))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = client.PostAsync(uri, content).Result;
                }
                contentString = await response.Content.ReadAsStringAsync();
            }
            return GetLinesFromJson(contentString);
        }

        static int getVal(string val)
        {
            return int.Parse(val.Split(',')[1]);
        }

        static OCRSearchResponse GetLinesFromJson(string jsonText)
        {
            List<string> allLines = new List<string>();
            List<string> webSites = new List<string>();
            List<string> tags = new List<string>();
            var data = (JObject)JsonConvert.DeserializeObject(jsonText);
            JArray regions = data["regions"].Value<JArray>();

            JArray ja = new JArray { regions.OrderBy(e => getVal(e["boundingBox"]?.Value<string>())).ToArray() };
            foreach (JObject region in ja.Children())
            {
                JArray regionLines = region["lines"].Value<JArray>();
                string line = "";
                foreach (JObject regionLine in regionLines.Children())
                {
                    JArray words = regionLine["words"].Value<JArray>();
                    foreach (JObject word in words.Children())
                    {
                        string w = word["text"]?.Value<string>();
                        if (!ComputerVisionServices.ExcludedWords.Contains(w.ToLower()))
                        {
                            if (onlyAlphabetRegex.IsMatch(w))
                            {
                                line = line + " " + w;
                                tags.Add(w);
                            }
                            if (websiteRegex.IsMatch(w))
                            {
                                webSites.Add(w);
                            }
                        }

                    }
                }
                if (!string.IsNullOrEmpty(line))
                {
                    allLines.Add(line);
                }
            }
            return new OCRSearchResponse()
            {
                Tags = tags,
                AllLines = allLines,
                WebSites = webSites
            };
            //return allLines;

        }

    }

    public class OCRSearchResponse
    {
        public List<string> AllLines { set; get; }
        public List<string> WebSites { set; get; }
        public List<string> Tags { set; get; }
    }
}
