﻿namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for GetInsights.xaml
    /// </summary>
    public partial class GetInsights : ThirdEyePage
    {
        public GetInsights(string imagePath, decimal longitude, decimal lattitude)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigateTo(new CrawlResults());
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigateBack();
        }
    }
}
