using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skrabbl.GameClient.Service;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient.Https
{
    public class HttpHelperResponse<TResult>
    {
        public HttpResponseMessage Response { get; set; }
        public TResult Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class HttpHelperHandler : HttpClientHandler
    {
        private bool _isRefreshing;
        private readonly SemaphoreSlim _isRefreshingSemaphore = new SemaphoreSlim(1, 1);

        private void InjectAuthorizationHeader(HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(DataContainer.Tokens?.Jwt?.Token)) return;

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", DataContainer.Tokens.Jwt.Token);
        }

        private async Task RefreshToken()
        {
            if (_isRefreshing) return;

            await _isRefreshingSemaphore.WaitAsync();
            try
            {
                if (_isRefreshing) return;

                _isRefreshing = true;
                await UserService.RefreshToken();
                _isRefreshing = false;
            }
            finally
            {
                _isRefreshingSemaphore.Release();
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // If JWT isn't valid :(
            if (DataContainer.IsTokenExpired())
            {
                // Hack to allow refresh URL to go through no matter what
                if (request.RequestUri.ToString().Contains("user/refresh"))
                {
                    var response = await base.SendAsync(request, cancellationToken);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Unable to refresh token. Token expired?");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponseDto>(content);
                    var tokens = ModelMapper.Mapper.Map<Tokens>(result);
                    UserService.SaveTokens(tokens);

                    return response;
                }

                await RefreshToken();
            }

            // Add JWT to header
            InjectAuthorizationHeader(request);

            // Everything is good :)
            return await base.SendAsync(request, cancellationToken);
        }
    }

    public static class HttpHelper
    {
        private static readonly int Port = 50916; //5001;

        private static readonly HttpClient Client = new HttpClient(new HttpHelperHandler())
        {
            BaseAddress = new Uri($"http://localhost:{Port}/api/")
        };

        public static async Task<HttpHelperResponse<TResult>> Post<TResult, TData>(string endpoint, TData data)
        {
            var serialize = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(serialize, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await Client.PostAsync(endpoint, httpContent);
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
            HttpResponseMessage resp = await Client.GetAsync(endpoint);
            string content = await resp.Content.ReadAsStringAsync();
            TResult result = default(TResult);

            try
            {
                result = JsonConvert.DeserializeObject<TResult>(content);
            }
            catch
            {
            }

            return new HttpHelperResponse<TResult>()
            {
                Response = resp,
                Result = result,
                StatusCode = resp.StatusCode
            };
          }
                // Ignored
        public static async Task<HttpHelperResponse<TResult>> Put<TResult, TData>(string endpoint, TData data)
        {
            var serialize = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(serialize, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await Client.PutAsync(endpoint, httpContent);

            string responseContent = await resp.Content.ReadAsStringAsync();

            TResult resultObject = default;

            try
            {
                resultObject = JsonConvert.DeserializeObject<TResult>(responseContent);
            }
            catch
            {
            }

            return new HttpHelperResponse<TResult>()
            {
                Response = resp,
                Result = resultObject,
                StatusCode = resp.StatusCode
            };
        }
    }
}
