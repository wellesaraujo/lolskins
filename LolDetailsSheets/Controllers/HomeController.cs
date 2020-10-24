using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LolDetailsSheets.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using LolDetailsSheets.Helper;

namespace LolDetailsSheets.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region HttpActions

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PrivacyAsync()
        {
            return View( await GetAsync());
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Methods
        private async Task<List<DetailDataViewModel>> GetAsync()
        {
            GoogleSheetHelper sheet = new GoogleSheetHelper();
            var serviceValues = sheet.GetSheetsService().Spreadsheets.Values;
            return await sheet.ReadAsync(serviceValues);
        }

        private async Task PutAsync([FromBody] DetailDataViewModel newData)
        {
            GoogleSheetHelper sheet = new GoogleSheetHelper();
            var serviceValues = sheet.GetSheetsService().Spreadsheets.Values;

            List<object> data = new List<object>
            {
                newData.CharName,
                newData.Type,
                newData.Skins,
                newData.Skin_Spotlight
            };
            var end = _configuration.GetValue<string>("DefaultSettings:Parameters:ColumnRange");
            string writeRange = "A" + newData.Id + ":" + end + newData.Id;
            await sheet.WriteAsync(serviceValues, data, writeRange);
        }
        #endregion
    }
}
