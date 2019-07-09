using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ThirdEye.Infrastructure.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        async Task<TResponse> IHttpHelper.GetAsync<TResponse>(string url, IDictionary<string, string> headers = null)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                    foreach (var header in headers) client.DefaultRequestHeaders.Add(header.Key, header.Value);

                client.BaseAddress = new System.Uri(url);
                var httpResponse = await client.GetAsync(url);
                var response = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode)
                    throw new System.Exception(response);

                var tResponse = JsonConvert.DeserializeObject<TResponse>(response);
                return tResponse;
            }
        }

     
        async Task<TResponse> IHttpHelper.PostAsync<TResponse, TRequest>(string url, TRequest request, IDictionary<string, string> headers = null, IDictionary<string, string> queryString = null)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                    foreach (var header in headers) client.DefaultRequestHeaders.Add(header.Key, header.Value);
                if (queryString != null)
                {
                    var queryStr = HttpUtility.ParseQueryString(string.Empty);
                    foreach (var query in queryString) queryStr[query.Key] = query.Value;
                    url += $"?{queryStr}";
                }

                client.BaseAddress = new System.Uri("https://eastus.api.cognitive.microsoft.com");
                var stringContent = new StringContent(JsonConvert.SerializeObject(request));
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var httpResponse = client.PostAsync(url, stringContent).Result;
                var response = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode)
                    throw new System.Exception(response);
                var tResponse = JsonConvert.DeserializeObject<TResponse>(response);
                return tResponse;
            }
        }
    }

    public interface IHttpHelper
    {
        Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, string> headers) where TResponse : class;
        Task<TResponse> PostAsync<TResponse, TRequest>(string url, TRequest request, IDictionary<string, string> headers, IDictionary<string, string> queryString = null);
    }
}
