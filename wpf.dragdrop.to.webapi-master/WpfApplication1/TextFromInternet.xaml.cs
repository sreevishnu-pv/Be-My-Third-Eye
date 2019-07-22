using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for CrawlResults.xaml
    /// </summary>
    public partial class TextFromInternet : ThirdEyePage
    {
        public TextFromInternet()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new Story());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }
    }
}
