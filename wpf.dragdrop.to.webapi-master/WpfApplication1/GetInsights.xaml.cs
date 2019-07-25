using System;
using System.Configuration;
using System.Windows.Documents;
using ThirdEye.Services.Services;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for GetInsights.xaml
    /// </summary>
    public partial class GetInsights : ThirdEyePage
    {
        public GetInsights()
        {
            ShowProgress();
            InitializeComponent();
            GetInsightsFromComputerVision();
            SetInsightOutput();
            HideProgress();
        }

        private void SetInsightOutput()
        {
            InsightsResults.Inlines.Add(Environment.NewLine);
            InsightsResults.Inlines.Add(new Bold(new Underline(new Run("Tags"))));
            InsightsResults.Inlines.Add(Environment.NewLine);
            InsightsResults.Inlines.Add(new Italic(new Run(string.Join(",", ComputerVisionServices.ImageSearchResponse.Tags))));
            InsightsResults.Inlines.Add(Environment.NewLine);
            InsightsResults.Inlines.Add(Environment.NewLine);
            InsightsResults.Inlines.Add(Environment.NewLine);
            InsightsResults.Inlines.Add(new Bold(new Underline(new Run("Search Query"))));
            InsightsResults.Inlines.Add(Environment.NewLine);
            InsightsResults.Inlines.Add(new Italic(new Run(ComputerVisionServices.ImageSearchResponse.SearchQuery)));
        }

        private void GetInsightsFromComputerVision()
        {
            var computerVisionSubscriptionKey = ConfigurationManager.AppSettings["ComputerVisionSubscription"];
            var ocrSubscriptionKey = ConfigurationManager.AppSettings["OCRSubscription"];
            var imageProcessor = new ImageProcessor(computerVisionSubscriptionKey, ocrSubscriptionKey);
            ComputerVisionServices.ImageSearchResponse = imageProcessor.ProcessImage(
                  ComputerVisionServices.Latitude,
                  ComputerVisionServices.Longitude,
                  ComputerVisionServices.FileName).GetAwaiter().GetResult();
        }

        private void CrawlWebButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigateTo(new CrawlResults());
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigateBack();
        }
    }
}
