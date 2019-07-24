
using System.Windows.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainFrame.xaml
    /// </summary>
    public partial class MainFrame : ThirdEyePage
    {

        public MainFrame()
        {
            InitializeComponent();
            this.DataContext = this;
            DynamicContent = _dynamicContent;
            if (CurrentPage == null)
            {
                ProgressBar = ThirdEyeProgressBar;
                CurrentPage = new ImageUpload();
                Navigate(CurrentPage);                
            }
        }

        public void Navigate(Page targetPage)
        {
            _dynamicContent.Navigate(targetPage);
        }
    }
}
