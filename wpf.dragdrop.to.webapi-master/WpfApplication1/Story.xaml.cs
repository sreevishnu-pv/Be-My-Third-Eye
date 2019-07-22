using System.Speech.Synthesis;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Story.xaml
    /// </summary>
    public partial class Story : ThirdEyePage
    {
        public Story()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var story = this.StoryContent.Text;
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Speak(story);
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
