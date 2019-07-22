namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for GetInsights.xaml
    /// </summary>
    public partial class GetInsights : ThirdEyePage
    {
        public GetInsights()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigateTo(new CrawlResults());
        }
    }
}
