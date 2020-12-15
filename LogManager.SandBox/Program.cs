using LogManager.Api.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace LogManager.SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadRequestLogFromFile();

            Console.WriteLine("Program finished, press any key to exit...");
            Console.ReadKey();
        }

        static void ReadRequestLogFromFile()
        {
            using (var stream = File.OpenRead(@"C:\log.txt"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                FromFileHelper.ReadRequestLogFromFile(file);
            }
        }
    }
}
