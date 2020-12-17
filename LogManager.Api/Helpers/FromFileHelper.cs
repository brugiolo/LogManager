using LogManager.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace LogManager.Api.Helpers
{
    public class FromFileHelper
    {
        public static IEnumerable<RequestLogViewModel> ReadRequestLogFromFile(IFormFile iFormFile)
        {
            var requestLogs = new List<RequestLogViewModel>();

            using (var streamReader = new StreamReader(iFormFile.OpenReadStream() ))
            {
                while (streamReader.Peek() >= 0)
                {
                    var line = streamReader.ReadLine();
                    var requestLog = ReadRequestLogFromLine(line.ToString());

                    requestLogs.Add(requestLog);
                }
            }

            return requestLogs;
        }

        private static RequestLogViewModel ReadRequestLogFromLine(string line)
        {
            var lineValues = line.Split(" ");

            return new RequestLogViewModel
            {
                Id = Guid.NewGuid(),
                Ip = ReadIpFromLine(lineValues),
                Adress = ReadAdressFromLine(lineValues),
                UserAgent = ReadUserAgentFromLine(lineValues),
                ContentLength = ReadContentLenghtFromLine(lineValues),
                DateTime = ReadDateTimeFromLine(lineValues),
                Method = ReadMethodFromLine(lineValues),
                Status = ReadStatusFromLine(lineValues)
            };
        }

        private static string ReadIpFromLine(string[] lineValues)
        {
            return lineValues[0].Trim();
        }

        private static string ReadAdressFromLine(string[] lineValues)
        {
            return lineValues[6].Trim();
        }

        private static string ReadUserAgentFromLine(string[] lineValues)
        {
            return lineValues[7].Replace(@"\", "").Replace("\"", "").Trim();
        }

        private static long ReadContentLenghtFromLine(string[] lineValues)
        {
            return lineValues[9] == "-" ? 0 : long.Parse(lineValues[9].Trim());
        }

        private static DateTime ReadDateTimeFromLine(string[] lineValues)
        {
            var dateInformation = lineValues[3].Replace("[", "");
            var timeInformation = lineValues[4].Replace("]", "");
            var dateTimeInformation = dateInformation + timeInformation;

            var utcDateTime = DateTime.ParseExact(dateTimeInformation, 
                "dd/MMM/yyyy:HH:mm:sszzz", CultureInfo.CurrentCulture).ToUniversalTime();

            return utcDateTime;
        }

        private static string ReadMethodFromLine(string[] lineValues)
        {
            return lineValues[5].Replace(@"\", "").Replace("\"", "").Trim();
        }

        private static int ReadStatusFromLine(string[] lineValues)
        {
            return int.Parse(lineValues[8].Trim());
        }
    }
}
