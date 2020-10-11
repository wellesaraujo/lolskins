using System.Net;
using System.Threading.Tasks;

namespace LolDetailsSheets.Interfaces.Services
{
    public interface IHttpService
    {
        Task<(bool IsSuccess, T ReturnObject, string Message, HttpStatusCode StatusCode)> PostToWebApi<T>(string url, object Params);

        Task<bool> GetFromWebApi(string url);
    }
}