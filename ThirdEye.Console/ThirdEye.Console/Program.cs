using Newtonsoft.Json;
using ThirdEye.Cognitive.Image.Tag;
using ThirdEye.Console.Infrastructure;

namespace ThirdEye.Console
{
    class Program
    {
        //http://images.mid-day.com/images/2017/oct/Aishwarya-Amitabh-ee.jpg
        //https://c8.alamy.com/comp/C11EJX/drummers-in-a-temple-thaipusam-festival-in-tenkasi-tamil-nadu-tamilnadu-C11EJX.jpg
        //https://media.cntraveler.com/photos/596ceda1f9e37f55cb0bfaf4/master/pass/chicago-botantic-garden-GettyImages-95607462.jpg
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
            var result = imageTagger.TagImage(imageUrl).Result;
            System.Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            System.Console.Read();
        }
    }
}
