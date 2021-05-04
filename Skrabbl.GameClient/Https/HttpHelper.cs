using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Skrabbl.GameClient.Https
{
    public class HttpHelperResponse<TResult>
    {
        public HttpResponseMessage Response { get; set; }
        public TResult Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    class HttpHelper
    {
        private static int port = 5001;

        private static HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri($"https://localhost:{port}/api/")
        };

        public static async Task<HttpHelperResponse<TResult>> Post<TResult, TData>(string endpoint, TData data)
        {
            var serialize = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(serialize, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync(endpoint, httpContent);
            string responseContent = await resp.Content.ReadAsStringAsync();
            TResult resultObject = default;

            if (data != null)
            {
                try
                {
                    resultObject = JsonConvert.DeserializeObject<TResult>(responseContent);
                }
                catch
                {
                }
            }

            return new HttpHelperResponse<TResult>()
            {
                Response = resp,
                Result = resultObject,
                StatusCode = resp.StatusCode
            };
        }

        public static Task<HttpHelperResponse<TResult>> Post<TResult>(string endpoint)
        {
            return Post<TResult, object>(endpoint, null);
        }

        public static async Task<HttpHelperResponse<TResult>> Get<TResult>(string endpoint)
        {
            HttpResponseMessage resp = await _client.GetAsync(endpoint);
            string content = await resp.Content.ReadAsStringAsync();
            TResult result = default(TResult);

            try
            {
                result = JsonConvert.DeserializeObject<TResult>(content);
            }
            catch
            {
                // Ignored
            }

            return new HttpHelperResponse<TResult>()
            {
                Response = resp,
                Result = result,
                StatusCode = resp.StatusCode
            };
        }
    }
}