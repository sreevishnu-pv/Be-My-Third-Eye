
using System.Windows.Controls;

namespace WpfApplication1
{
    public class ThirdEyePage : Page
    {
        public static Page CurrentPage;
        public static Frame DynamicContent;

        public ThirdEyePage()
        {

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
