using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for CrawlResults.xaml
    /// </summary>
    public partial class CrawlResults : ThirdEyePage
    {
        public CrawlResults()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new Story());
        }
    }
}
