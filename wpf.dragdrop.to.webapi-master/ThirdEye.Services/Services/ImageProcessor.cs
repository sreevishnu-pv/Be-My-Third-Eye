using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThirdEye.Services.Models;

namespace ThirdEye.Services.Services
{
    public class ImageProcessor
    {
        private OCRImageProcessor orcProcessor;
        private VisionImageProcessor visionProcessor;

        private static Dictionary<(decimal, decimal), string> _locations = new Dictionary<(decimal, decimal), string>()
        {
            { (16.515393M, 80.605873M), "Sri Durga Malleswara Swamy Varla Devasthanam"},
            { (17.429892M,78.341136M),"Hyderabad"}

        };
        public ImageProcessor(string visionSubscriptionKey, string ocrSubscriptionKey)
        {
            orcProcessor = new OCRImageProcessor(ocrSubscriptionKey);
            visionProcessor = new VisionImageProcessor(visionSubscriptionKey);
        }

        public async Task<ImageSearchResponse> ProcessImage(decimal latitude, decimal longitude, string imagePath)
        {
            byte[] imageBytes = GetImageAsByteArray(imagePath);
            OCRSearchResponse ocrResponse = await orcProcessor.GetOCRText(imageBytes);
            string location = GetLocationByPoint(latitude, longitude);
            if (ocrResponse.Tags.Any())
            {
                return CreateImageSearchResponse(location, ocrResponse);
            }

            List<string> tags = await visionProcessor.GetImageTags(imagePath);
            return CreateImageSearchResponse(location, tags);
        }

        private string GetLocationByPoint(decimal latitude, decimal longitude)
        {
            if (_locations.ContainsKey((latitude, longitude)))
                return _locations[(latitude, longitude)];
            return "Hyderabad";
        }

        private ImageSearchResponse CreateImageSearchResponse(string location, OCRSearchResponse ocrResponse)
        {
            ImageSearchResponse searchResponse = new ImageSearchResponse();
            searchResponse.Tags = ocrResponse.Tags;
            searchResponse.WebSites = ocrResponse.WebSites;
            searchResponse.SearchQuery = $"{location} {(string.Join(" ", ocrResponse.Tags))}";
            return searchResponse;
        }

        private ImageSearchResponse CreateImageSearchResponse(string location, List<string> tags)
        {
            ImageSearchResponse searchResponse = new ImageSearchResponse();
            searchResponse.Tags = tags;
            searchResponse.SearchQuery = location + " " + (string.Join(" ", tags));
            return searchResponse;
        }

        public static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }
}
