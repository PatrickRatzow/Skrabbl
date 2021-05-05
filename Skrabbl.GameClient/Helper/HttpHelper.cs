using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skrabbl.GameClient.Service;
using Skrabbl.Model;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient.Helper
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
        public static readonly int Port = 5001; //5001
        public static readonly string Url = $"https://localhost:{Port}";

        private static readonly HttpClient Client = new HttpClient(new HttpHelperHandler())
        {
            BaseAddress = new Uri($"{Url}/api/")
        };

        public static async Task<HttpHelperResponse<TResult>> Post<TResult, TData>(string endpoint, TData data)
        {
            var httpContent = Serialize(data);
            HttpResponseMessage resp = await Client.PostAsync(endpoint, httpContent);

            return await ParseResponse<TResult>(resp);
        }

        public static async Task<HttpHelperResponse<TResult>> Get<TResult>(string endpoint)
        {
            HttpResponseMessage resp = await Client.GetAsync(endpoint);

            return await ParseResponse<TResult>(resp);
        }

        public static async Task<HttpHelperResponse<TResult>> Put<TResult, TData>(string endpoint, TData data)
        {
            var httpContent = Serialize(data);
            HttpResponseMessage resp = await Client.PutAsync(endpoint, httpContent);

            return await ParseResponse<TResult>(resp);
        }


        private static TResult Deserialize<TResult>(string data)
        {
            if (!typeof(TResult).IsClass || typeof(TResult) == typeof(string))
            {
                return (TResult) (object) data;
            }

            var result = default(TResult);
            try
            {
                result = JsonConvert.DeserializeObject<TResult>(data);
            }
            catch
            {
                // Ignored
            }

            return result;
        }

        private static async Task<HttpHelperResponse<TResult>> ParseResponse<TResult>(HttpResponseMessage resp)
        {
            var content = await resp.Content.ReadAsStringAsync();
            var result = Deserialize<TResult>(content);

            return new HttpHelperResponse<TResult>()
            {
                Response = resp,
                Result = result,
                StatusCode = resp.StatusCode
            };
        }

        private static StringContent Serialize<TData>(TData data)
        {
            var serialize = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(serialize, Encoding.UTF8, "application/json");

            return httpContent;
        }
    }
}