
using System.Windows.Controls;

namespace WpfApplication1
{
    public class ThirdEyePage : Page
    {
        public static Page CurrentPage;
        public static Frame DynamicContent;
        public static ProgressBar ProgressBar;

        public ThirdEyePage()
        {

        }

        public void ShowProgress()
        {
            ProgressBar.Visibility = System.Windows.Visibility.Visible;
        }

        public void HideProgress()
        {
            ProgressBar.Visibility = System.Windows.Visibility.Hidden;
        }

        public void NavigateTo(Page targetPage)
        {
            Navigate(DynamicContent, targetPage);
        }
        public void NavigateBack()
        {
            DynamicContent.NavigationService.GoBack();
        }

        private void Navigate(Frame sourceFrame, Page targetPage)
        {
            sourceFrame.Navigate(targetPage);
        }
    }
}
