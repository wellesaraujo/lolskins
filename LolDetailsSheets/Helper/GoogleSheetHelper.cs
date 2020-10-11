using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using LolDetailsSheets.Models;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LolDetailsSheets.Helper
{
    public class GoogleSheetHelper
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string SpreadsheetId = "1tdEpkl3wFGopwPw4_f3my2hNiV_-rLK-9GFYGj4ghN8";
        private const string GoogleCredentialsFileName = "google-credentials.json";
        private const string ReadRange = "Sheet1!A:D";

        public SheetsService GetSheetsService()
        {
            using var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read);
            var serviceInitializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
            };
            return new SheetsService(serviceInitializer);
        }

        public async Task<List<DetailDataViewModel>> ReadAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            Google.Apis.Sheets.v4.Data.ValueRange response = await valuesResource.Get(SpreadsheetId, ReadRange).ExecuteAsync();
            IList<IList<object>> values = response.Values;

            DetailData detail = new DetailData();

            if (values != null || values.Any())
            {
                //CABEÇALHO: var header = string.Join(" ", values.First().Select(r => r.ToString()));
                //foreach (var row in values.Skip(1))
                for (int i = 1; i < 4; i++)
                {
                    var row = values[i];
                    detail.Data.Add(new DetailDataViewModel
                    {
                        Id = i,
                        CharName = row[0].ToString(),
                        Type = row[1].ToString(),
                        Skins = row[2].ToString(),
                        Skin_Spotlight = row[3].ToString(),
                    });
                }
            }
            return detail.Data;
        }

        public async Task WriteAsync(SpreadsheetsResource.ValuesResource valuesResource, List<object> newData, string writeRange)
        {
            var valueRange = new ValueRange { Values = new List<IList<object>> { newData } };
            var update = valuesResource.Update(valueRange, SpreadsheetId, writeRange);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            var response = await update.ExecuteAsync();
        }
    }
}