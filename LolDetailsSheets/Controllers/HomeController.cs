using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LolDetailsSheets.Models;
using System.Threading.Tasks;
using LolDetailsSheets.Services;
using LolDetailsSheets.Interfaces.Services;

namespace LolDetailsSheets.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpService _httpService;
        public HomeController(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var ok = await _httpService.GetFromWebApi("https://localhost:44342/api/values");
            return View(ok);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
