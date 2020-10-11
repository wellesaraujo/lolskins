using System.Collections.Generic;
using System.Threading.Tasks;
using LolDetailsSheets.Helper;
using LolDetailsSheets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LolDetailsSheets.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase

    {
        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<List<DetailDataViewModel>> GetAsync()
        {
            GoogleSheetHelper sheet = new GoogleSheetHelper();
            var serviceValues = sheet.GetSheetsService().Spreadsheets.Values;
            return await sheet.ReadAsync(serviceValues);
        }

        [HttpPut]
        public async Task PutAsync([FromBody] DetailDataViewModel newData)
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
            string writeRange = "A" + newData.Id + ":"+end+ newData.Id;
            await sheet.WriteAsync(serviceValues, data, writeRange);
        }
    }
}
