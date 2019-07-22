using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
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

            //GetMetaData getMetaDataWindow = new GetMetaData();
            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(getMetaDataWindow);


            //this.DynamicContent = getMetaDataWindow.MetaData;
            //this.DynamicContent.Refresh();
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    _files.Clear();

            //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            //    foreach (string filePath in files)
            //    {
            //        _files.Add(filePath);
            //    }

            //    UploadFiles(files);
            //}

            //var listbox = sender as ListBox;
            //listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
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

        private void UploadFiles(string[] files)
        {
            ProgressMessageHandler progress = new ProgressMessageHandler();
            //progress.HttpSendProgress += new EventHandler<HttpProgressEventArgs>(HttpSendProgress);

            HttpRequestMessage message = new HttpRequestMessage();
            MultipartFormDataContent content = new MultipartFormDataContent();

            try
            {

                foreach (var file in files)
                {
                    FileStream filestream = new FileStream(file, FileMode.Open);
                    string fileName = System.IO.Path.GetFileName(file);
                    content.Add(new StreamContent(filestream), "file", fileName);
                }

                message.Method = HttpMethod.Post;
                message.Content = content;
                message.RequestUri = new Uri("http://localhost:51884/api/uploading/");

                //ThreadSafeUpdateStatus(String.Format("Uploading {0} files", files.Count()));

                var client = HttpClientFactory.Create(progress);
                client.SendAsync(message).ContinueWith(task =>
                {
                    if (task.Result.IsSuccessStatusCode)
                    {
                        // ThreadSafeUpdateStatus(String.Format("Uploaded {0} files", files.Count()));
                        var response = task.Result.Content.ReadAsStringAsync();
                        dynamic json = JsonConvert.DeserializeObject<List<FileDesc>>(response.Result);

                        foreach (var item in json)
                        {
                            var listitem = _files.FirstOrDefault(i => i.Contains(item.name));

                            Application.Current.Dispatcher.Invoke(
                                DispatcherPriority.Normal, (Action)delegate ()
                                {
                                    _files.Remove(listitem);
                                });

                            listitem += String.Format(" - successfully uploaded. Size: {0}", item.size);

                            Application.Current.Dispatcher.Invoke(
                                DispatcherPriority.Normal, (Action)delegate ()
                                {
                                    _files.Add(listitem);
                                });
                        }
                    }
                    else
                    {
                        //ThreadSafeUpdateStatus("Sorry there has been an error");
                    }
                });

            }
            catch (Exception e)
            {
                //Handle exceptions - file not found, access denied, no internet connection, threading issues etc etc
            }
        }

        //private void HttpSendProgress(object sender, HttpProgressEventArgs e)
        //{
        //    HttpRequestMessage request = sender as HttpRequestMessage;
        //    ProgressBar.Dispatcher.BeginInvoke(
        //            DispatcherPriority.Normal, new DispatcherOperationCallback(delegate
        //            {
        //                ProgressBar.Value = e.ProgressPercentage;
        //                return null;
        //            }), null);
        //}

        //private void ThreadSafeUpdateStatus(string status)
        //{
        //    StatusIndicator.Dispatcher.BeginInvoke(
        // DispatcherPriority.Normal, new DispatcherOperationCallback(delegate
        // {
        //     StatusIndicator.Text = status;
        //     return null;
        // }), null);
        //}

        private void ButtonAnalyze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new GetInsights());
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            bool? result = dlg.ShowDialog();
            string path;
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
