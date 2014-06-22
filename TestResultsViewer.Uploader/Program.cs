using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace TestResultsViewer.Uploader
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) { throw new ArgumentException("At least one file should be specified."); }

            foreach (var filename in args)
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine("ERROR: File does not exists: {0}", filename);
                    continue;
                }

                var testsResultsViewerUrl = new Uri(ConfigurationManager.AppSettings["TestResultsViewerUrl"]);

                var client = new WebClient();

                try
                {
                    client.UploadFile(testsResultsViewerUrl, filename);
                    Console.WriteLine("SUCCESS: File upload succeed {0}", filename);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("ERROR: Error uploading file: {0}; Message: {1}", filename, ex.Message);
                }
                
            }
        }
    }
}
