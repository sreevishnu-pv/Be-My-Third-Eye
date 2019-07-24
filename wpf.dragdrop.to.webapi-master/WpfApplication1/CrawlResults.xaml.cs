using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ThirdEye.Services.Services;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for CrawlResults.xaml
    /// </summary>
    public partial class CrawlResults : ThirdEyePage
    {
        public CrawlResults()
        {
            ShowProgress();
            InitializeComponent();
            ComputerVisionServices.BingWebSearchResult = new BingWebSearchService().GetWebSearchResults(ComputerVisionServices.ImageSearchResponse.SearchQuery);
            SetWebSearchOutput();
            HideProgress();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new TextFromInternet());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }
        private void SetWebSearchOutput()
        {
            if (ComputerVisionServices.BingWebSearchResult.RelatedLinks.Any())
            {
                WebSearchResults.Inlines.Add(Environment.NewLine);
                WebSearchResults.Inlines.Add(new Bold(new Underline(new Run("Related Links"))));
                WebSearchResults.Inlines.Add(Environment.NewLine);
                ComputerVisionServices.BingWebSearchResult.RelatedLinks.ForEach(link =>
                {
                    WebSearchResults.Inlines.Add(new Italic(new Run(link)));
                    WebSearchResults.Inlines.Add(Environment.NewLine);
                });
                WebSearchResults.Inlines.Add(Environment.NewLine);
                WebSearchResults.Inlines.Add(Environment.NewLine);
            }
            WebSearchResults.Inlines.Add(Environment.NewLine);
            if (ComputerVisionServices.BingWebSearchResult.RelatedVideos.Any())
            {
                WebSearchResults.Inlines.Add(new Bold(new Underline(new Run("Related Videos"))));
                WebSearchResults.Inlines.Add(Environment.NewLine);
                ComputerVisionServices.BingWebSearchResult.RelatedVideos.ForEach(video =>
                {
                    WebSearchResults.Inlines.Add(new Italic(new Run(video)));
                    WebSearchResults.Inlines.Add(Environment.NewLine);
                });
                WebSearchResults.Inlines.Add(Environment.NewLine);
                WebSearchResults.Inlines.Add(Environment.NewLine);
            }
            WebSearchResults.Inlines.Add(Environment.NewLine);
            if (ComputerVisionServices.BingWebSearchResult.Snippets.Any())
            {
                WebSearchResults.Inlines.Add(new Bold(new Underline(new Run("Related Snippets"))));
                WebSearchResults.Inlines.Add(Environment.NewLine);
                ComputerVisionServices.BingWebSearchResult.Snippets.ForEach(snippet =>
                {
                    WebSearchResults.Inlines.Add(new Italic(new Run(snippet)));
                    WebSearchResults.Inlines.Add(Environment.NewLine);
                });
            }
        }


    }
}
