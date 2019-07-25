using Boilerpipe.Net.Extractors;
using HtmlAgilityPack;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ThirdEye.Services.Services
{
    public class WebScrappingService
    {
        private TextAnalyticsClient _textAnalyticsClient;
        public WebScrappingService()
        {
            _textAnalyticsClient = new TextAnalyticsClient(new Analytics(ConfigurationManager.AppSettings["WebScrapApi"]));
            _textAnalyticsClient.Endpoint = ConfigurationManager.AppSettings["TextAnalyticsEndpoint"];
        }
        public List<string> GetParagraphsFromUri(string uri)
        {
            var paragraphs = new List<string>();
            var htmlDocument = new HtmlWeb().Load(uri);
            string rawText = CommonExtractors.DefaultExtractor.GetText(htmlDocument.ParsedText);
            paragraphs = rawText.Split('\n').ToList();
            //paragraphs = FilterEnglishParagraphs(paragraphs);
            return paragraphs;
        }

        public List<string> FilterEnglishParagraphs(List<string> paragraphs)
        {
            var englishParagraphs = new List<string>();
            var paras = new List<LanguageInput>();
            for (int i = 0; i < paragraphs.Count; i++)
            {
                paras.Add(new LanguageInput(id: i.ToString(), text: paragraphs[i]));
            }
            var inputDocuments = new LanguageBatchInput(paras);
            var langResults = _textAnalyticsClient.DetectLanguageAsync(false, inputDocuments).Result;


            foreach (var res in langResults.Documents)
            {
                if (res.DetectedLanguages[0].Name == "English" && paragraphs[System.Convert.ToInt32(res.Id)].Split(' ').Length > 7)
                {
                    englishParagraphs.Add(paragraphs[System.Convert.ToInt32(res.Id)]);
                }
            }
            return englishParagraphs;
        }


    }
}
