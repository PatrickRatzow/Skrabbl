using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.GameClient.Https
{
    public class HttpHelperResponse<TResult> {
        public HttpResponseMessage Response { get; set; }
        public TResult Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    class HttpHelper
    {
        private static int port = 50916;
        private static HttpClient _client = new HttpClient() {
            BaseAddress = new Uri($"http://localhost:{port}/api/")
        };

        public static async Task<HttpHelperResponse<TResult>> Post<TResult, TData>(string endpoint, TData data)
        {
            var serialize = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(serialize, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync(endpoint, httpContent);

            string responseContent = await resp.Content.ReadAsStringAsync();

            TResult resultObject = default;

            try
            {
                resultObject = JsonConvert.DeserializeObject<TResult>(responseContent);

            } catch { 
            }

            return new HttpHelperResponse<TResult>() {
                Response = resp,
                Result = resultObject,
                StatusCode = resp.StatusCode
            };
        }
    }
}
