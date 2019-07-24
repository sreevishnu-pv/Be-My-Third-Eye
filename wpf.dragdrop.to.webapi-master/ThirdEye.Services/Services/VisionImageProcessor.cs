using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdEye.Services.Services
{
    public class VisionImageProcessor
    {
        private static readonly List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
        {
            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
            VisualFeatureTypes.Tags
        };

        readonly string _subscriptionKey;
        public VisionImageProcessor(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }

        public async Task<List<string>> GetImageTags(string imagePath)
        {
            ComputerVisionClient computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(_subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });

            computerVision.Endpoint = "https://eastus.api.cognitive.microsoft.com";
            return await GetTagsFromImageFromCognitive(computerVision, imagePath);
        }

        public static async Task<List<string>> GetTagsFromImageFromCognitive(
            ComputerVisionClient computerVision, string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine(
                    "\nUnable to open or read localImagePath:\n{0} \n", imagePath);
                return new List<string>(); ;
            }

            using (Stream imageStream = File.OpenRead(imagePath))
            {
                ImageAnalysis analysis = computerVision.AnalyzeImageInStreamAsync(
                    imageStream, features).Result;
                return analysis.Tags.Where(x => !ComputerVisionServices.ExcludedWords.Contains(x.Name.ToLower())).Select(t => t.Name).ToList();
            }
        }
    }
}
