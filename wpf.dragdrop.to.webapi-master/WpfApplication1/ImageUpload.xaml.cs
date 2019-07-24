using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ThirdEye.Services.Services;

namespace WpfApplication1
{
    public partial class ImageUpload : ThirdEyePage
    {
        public string path = string.Empty;

        public ImageUpload()
        {
            InitializeComponent();
            
            this.DataContext = this;
        }

        private void GetInsightsButtonClick(object sender, RoutedEventArgs e)
        {
            ComputerVisionServices.Latitude = Convert.ToDecimal(Lattitude.Text);
            ComputerVisionServices.Longitude = Convert.ToDecimal(Longitude.Text);
            //ShowProgress();
            NavigateTo(new GetInsights());
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                path = dlg.FileName;
                BitmapImage source = new BitmapImage(new Uri(path));
                BrowsedImage.Source = source;
                ComputerVisionServices.FileName = path;
            }
            else
            {
                // user did not select image
                return;
            }
        }
    }
}
