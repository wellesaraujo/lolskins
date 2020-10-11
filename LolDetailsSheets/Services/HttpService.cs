using LolDetailsSheets.Interfaces.Services;
using LolDetailsSheets.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LolDetailsSheets.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool IsSuccess, T ReturnObject, string Message, HttpStatusCode StatusCode)> PostToWebApi<T>(string url, object Params)
        {
            Uri requestUrl = new Uri(url);
            
            string json = JsonConvert.SerializeObject(Params);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(requestUrl.ToString(), content);

            return (await DesearilizeResult<T>(result).ConfigureAwait(true));
        }

        public async Task<bool> GetFromWebApi(string url)
        {
            Uri requestUrl = new Uri(url);
            var response = await _httpClient.GetAsync(requestUrl.ToString());

            return response.IsSuccessStatusCode;
        }

        private async Task<(bool IsSucess, T ReturnObject, string Message, HttpStatusCode StatusCode)> DesearilizeResult<T>(HttpResponseMessage result)
        {
            var obj = default(T);
            var stream = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                obj = JsonConvert.DeserializeObject<T>(stream);
            }

            return (result.IsSuccessStatusCode, obj, stream, result.StatusCode);
        }
    }
}