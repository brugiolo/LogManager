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
                Ip = ReadIpFromLine(lineValues),
                Adress = ReadAdressFromLine(lineValues),
                Client = ReadClientFromLine(lineValues),
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

        private static string ReadClientFromLine(string[] lineValues)
        {
            return lineValues[7].Trim();
        }

        private static long ReadContentLenghtFromLine(string[] lineValues)
        {
            return lineValues[9] == "-" ? 0 : long.Parse(lineValues[9].Trim());
        }

        private static DateTime ReadDateTimeFromLine(string[] lineValues)
        {
            var dateTimeInformation = lineValues[3].Replace("[", "").Split(":");

            var dateTime = DateTime.ParseExact("04/Jan/2003:14:56:50 +0200", "dd/MMM/yyyy:HH:mm:ss zzz", CultureInfo.InvariantCulture);

            var dateTimeValue = DateTime.Parse(dateTimeInformation[0]);

            var date = dateTimeInformation[0];
            var hour = dateTimeInformation[1];
            var minute = dateTimeInformation[2];
            var seconds = dateTimeInformation[3];
            var timeZone = lineValues[4].Replace("]", "").Trim();

            return new DateTime();
        }

        private static string ReadMethodFromLine(string[] lineValues)
        {
            return lineValues[5].Trim();
        }

        private static int ReadStatusFromLine(string[] lineValues)
        {
            return int.Parse(lineValues[8].Trim());
        }
    }
}
