using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace TestResultsViewer.Uploader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) { throw new ArgumentException("File path should be specified.") ;}
            string fileName = args[0];

            if (!File.Exists(fileName)) { throw new ArgumentException("File must exists"); }

            var testsResultsViewerUrl = new Uri(ConfigurationManager.AppSettings["TestResultsViewerUrl"]);
            var client = new WebClient();

            client.UploadFile(testsResultsViewerUrl, fileName);

            Console.WriteLine("Upload done");
        }
    }
}
