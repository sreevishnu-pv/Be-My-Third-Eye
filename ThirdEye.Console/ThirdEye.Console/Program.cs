using Newtonsoft.Json;
using ThirdEye.Cognitive.Image.Tag;
using ThirdEye.Console.Infrastructure;

namespace ThirdEye.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("###################################");
            System.Console.WriteLine("~~~~ Third Eye Image Analyzer ~~~~~");
            System.Console.WriteLine("###################################");
            System.Console.WriteLine();
            System.Console.WriteLine("Provide the image url : ");
            var imageUrl = System.Console.ReadLine();            

            Bootstraper.Initialize();
            var imageTagger = (IImageTagger)Bootstraper.ServiceProvider.GetService(typeof(IImageTagger));
            var result = imageTagger.TagImage(imageUrl);
            System.Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            System.Console.Read();
        }
    }
}
