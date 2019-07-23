using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ImageUpload : ThirdEyePage
    {
        public string path = string.Empty;
        public ObservableCollection<string> Files
        {
            get
            {
                return _files;
            }
        }

        private ObservableCollection<string> _files = new ObservableCollection<string>();

        public ImageUpload()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        private void DropBox_Drop(object sender, DragEventArgs e)
        {

           
        }

        private void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as ListBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

    

        private void ButtonAnalyze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal longitude = Convert.ToDecimal(Longitude.Text);
            decimal lattitude = Convert.ToDecimal(Lattitude.Text);
            NavigateTo(new GetInsights(path,longitude,lattitude));
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
            }
            else
            {
                // user did not select image
                return;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
