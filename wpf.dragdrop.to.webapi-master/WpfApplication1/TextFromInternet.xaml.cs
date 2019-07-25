using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Documents;
using ThirdEye.Services.Services;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for CrawlResults.xaml
    /// </summary>
    public partial class TextFromInternet : ThirdEyePage
    {
        
        public TextFromInternet()
        {
            ShowProgress();
            InitializeComponent();
            var webScrappingService = new WebScrappingService();
            ComputerVisionServices.BingWebSearchResult.RelatedLinks.ForEach(link =>
            {
                ComputerVisionServices.ParagraphsFromInternet.Add(link, webScrappingService.GetParagraphsFromUri(link));
            });
            SetOutputForParagraphs();
            HideProgress();
        }

        private void SetOutputForParagraphs()
        {
            foreach (var link in ComputerVisionServices.ParagraphsFromInternet)
            {
                TextsFromInternet.Inlines.Add(Environment.NewLine);
                TextsFromInternet.Inlines.Add(new Bold(new Underline(new Run(link.Key))));
                TextsFromInternet.Inlines.Add(Environment.NewLine);
                TextsFromInternet.Inlines.Add(new Run(string.Join(Environment.NewLine, link.Value)));
                TextsFromInternet.Inlines.Add(Environment.NewLine);
                TextsFromInternet.Inlines.Add(Environment.NewLine);
            }
        }
        private void GetStoryButtonClick(object sender, RoutedEventArgs e)
        {
            var fileService = new FileService();
            //write input files for py
            int count = 0;
            foreach (var link in ComputerVisionServices.ParagraphsFromInternet)
            {
                fileService.WriteParagraphs(link.Value, $"link{count}.txt", ThirdEye.Services.FileTypeEnum.Input);
                count++;
            }

            //write tag files for py
            var tagsText = new List<string>();
            ComputerVisionServices.ImageSearchResponse.Tags.ForEach(tag =>
            {
                tagsText.Add($"'{tag}'");
            });
            var contentToWrite = $"[{string.Join(",", tagsText)}]";
            fileService.WriteText(contentToWrite, "Tags.txt", ThirdEye.Services.FileTypeEnum.Tags);
            NavigateTo(new Story());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }
    }
}
