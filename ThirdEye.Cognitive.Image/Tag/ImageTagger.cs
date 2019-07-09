using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThirdEye.Cognitive.Image.Models;
using ThirdEye.Infrastructure;
using ThirdEye.Infrastructure.Helpers;

namespace ThirdEye.Cognitive.Image.Tag
{
    public class ImageTagger : IImageTagger
    {
        private IHttpHelper _httpHelper;
        private IOptions<AppSettings> _appSettings;
        private IKeyVaultHelper _keyVaultHelper;

        public ImageTagger(IHttpHelper httpHelper, IOptions<AppSettings> appSettings, IKeyVaultHelper keyVaultHelper)
        {
            _httpHelper = httpHelper;
            _appSettings = appSettings;
            _keyVaultHelper = keyVaultHelper;
        }

        async Task<ImageTagResponse> IImageTagger.TagImage(string imageUrl)
        {
            var subscriptionKey = await _keyVaultHelper.GetSecretAsync(_appSettings.Value.CognitiveServices.ComputerVisionSubscriptionKey);

            var headers = new Dictionary<string, string>
            {
                { "Ocp-Apim-Subscription-Key",subscriptionKey }
            };

            var queryString = new Dictionary<string, string>
            {
                { "visualFeatures","tags,Categories,Description,Faces,ImageType,Color,Adult"},
                { "details","Celebrities,Landmarks"},
                { "language","en"}
            };

            var requestModel = new ImageTagHttpRequestModel { Url = imageUrl };
            var imageTagResponse = await _httpHelper.PostAsync<ImageTagResponse, ImageTagHttpRequestModel>(_appSettings.Value.CognitiveServices.ComputerVisionApiEndpoint, requestModel, headers, queryString);
            return imageTagResponse;
        }
    }

    public interface IImageTagger
    {
        Task<ImageTagResponse> TagImage(string imageUrl);
    }
}
