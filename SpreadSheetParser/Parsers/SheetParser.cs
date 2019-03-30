using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;

namespace SpreadSheetParser.Parsers
{
    public class SheetParser
    {
        private static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static string ApplicationName = "WebQuiz";

        private readonly UserCredential credential;
        private readonly string credentialPath = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/token";
        private readonly SheetsService service;
        private static readonly string sheetId = "1FgbLOKoa1FuXnyiDLsweLoAtU60u3i0MlnMLJRCnE38";

        public SheetParser()
        {
            
            using (var stream = new FileStream($"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credentialPath, true)).Result;
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }


        public IList<IList<object>> GetValues(string range)
        {
            var request = service.Spreadsheets.Values.Get(sheetId, range);
            var response = request.Execute();
            var values = response.Values;
            return values;
        }

    }
}
