using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using HtmlAgilityPack;
using Boilerpipe.Net.Extractors;
using WebScraper;

namespace WebScraperTest
{
    class Program
    {
         
        static void Main(string[] args)
        {
            //var url = "https://www.thehindu.com//news//cities//Vijayawada//shakhambari-devi-festival-begins//article24514788.ece";
            //var url = "https://deals.fullhyderabad.com/american-tourister-luggage-store-discount/end-of-season-sale-upto-40-off-on-american-tourister-luggage/deal-details-reviews-76494-1.html";
            var url = "https://www.indiainfoline.com//article//general-others-factiva//samsonite-american-tourister-and-high-sierra-announces-end-of-season-sale-116063000061_1.html";
            //var url = "http://kanakadurgamma.org/";
            var web = new HtmlWeb();
            
            var doc = web.Load(url);
            string text = CommonExtractors.DefaultExtractor.GetText(doc.ParsedText);
            string[] paragraphs = text.Split('\n');

            string ApiKey = "d4928f2c348441a1b1a3f02be2804d55";
            string Endpoint = "https://southeastasia.api.cognitive.microsoft.com";
            Analytics credentials = new Analytics(ApiKey);
            var client = new TextAnalyticsClient(credentials)
            {
                Endpoint = Endpoint
            };

            // Change the console encoding to display non-ASCII characters.
            List<string> EnglishParas= GetEnglishParas(client, paragraphs).Result;

        }
        public static async Task<List<string>> GetEnglishParas(TextAnalyticsClient client, string[] paragraphs)
        {

            // The documents to be submitted for language detection. The ID can be any value.
            List<LanguageInput> paras = new List<LanguageInput>
            {

            };
            for (int i = 0; i < paragraphs.Length; i++)
            {
                paras.Add(new LanguageInput(id: i.ToString(), text: paragraphs[i]));
            }
            var inputDocuments = new LanguageBatchInput(paras);
            //...
            var langResults = await client.DetectLanguageAsync(false, inputDocuments);

            // Printing detected languages
            List<string> English_paras = new List<string>();
            foreach (var res in langResults.Documents)
            {
                if (res.DetectedLanguages[0].Name == "English" && paragraphs[System.Convert.ToInt32(res.Id)].Split(' ').Length>7)
                {
                    English_paras.Add(paragraphs[System.Convert.ToInt32(res.Id)]);
                }
            }
            return English_paras;

        }
    }

    //class BlogScraper : WebScraper
    //{
    //    public override void Init()
    //    {
    //        this.LoggingLevel = WebScraper.LogLevel.All;
    //        this.Request("http://kanakadurgamma.org/", Parse);
    //    }
    //    public override void Parse(Response response)
    //    {
            
            //var result = Uglify.HtmlToText(response.Html);
            //Console.WriteLine(result.Code);

            //ScrapingBrowser browser = new ScrapingBrowser();
            //WebPage homePage = browser.NavigateToPage(new Uri("https://www.thehindu.com//news//cities//Vijayawada//shakhambari-devi-festival-begins//article24514788.ece"));


            //System.IO.File.WriteAllText(@"C:\Temp\bbc.html", homePage.Content);

            //var jsonConfig = File.ReadAllText(@"C:\Temp\bbcjson.json");
            //var config = StructuredDataConfig.ParseJsonString(jsonConfig);

            //var html = File.ReadAllText(@"C:\Temp\bbc.html", Encoding.UTF8);

            //var openScraping = new StructuredDataExtractor(config);
            ////var scrapingResults = openScraping.Extract(html);
            //var scrapingResults = openScraping.Extract(html);

            //Console.WriteLine(JsonConvert.SerializeObject(scrapingResults, Newtonsoft.Json.Formatting.Indented));

            //foreach (var title_link in response.Css("h2.entry-title a"))
            //{
            //    string strTitle = title_link.TextContentClean;
            //    Scrape(new ScrapedData() { { "Title", strTitle } });
            //}
            //if (response.CssExists("div.prev-post > a[href]"))
            //{
            //    var next_page = response.Css("div.prev-post > a[href]")[0].Attributes["href"];
            //    this.Request(next_page, Parse);
            //}
      


}
