using System;
using System.Configuration;
using System.Windows;
using ThirdEye.Services.Services;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Story.xaml
    /// </summary>
    public partial class Story : ThirdEyePage
    {
        public bool ExecuteFromPy = Convert.ToBoolean(ConfigurationManager.AppSettings["ExecuteFromPy"]);
        public Story()
        {
            InitializeComponent();
            SetStoryContent();
        }

        private void PublishStoryButtonClick(object sender, RoutedEventArgs e)
        {
            string title = ComputerVisionServices.ImageSearchResponse.SearchQuery == "SriDurgaMalleswaraSwamyVarlaDevasthanamfestivalcarnivaltradition" ? "Durga Temple" : "Aparna Construction";
            new DocumentDBService().InsertDocument
                (title,
                ComputerVisionServices.Story,
                ComputerVisionServices.BingWebSearchResult.RelatedLinks,
                ComputerVisionServices.BingWebSearchResult.RelatedVideos,
                ComputerVisionServices.ImageSearchResponse.Location
                );
        }


        private void SetStoryContent()
        {
            if (!ExecuteFromPy)
            {
                StoryContent.Text = new FileService().ReadFile($"{ComputerVisionServices.ImageSearchResponse.SearchQuery.Replace(" ", "")}.txt", ThirdEye.Services.FileTypeEnum.Output); ;
            }
            else
            {
                var text = new PythonProcessingService().ExecuteExe();
            }
            ComputerVisionServices.Story = StoryContent.Text;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
