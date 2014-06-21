using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace TestResultsViewer.Uploader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) { throw new ArgumentException("At least one file should be specified."); }

            foreach (var filename in args)
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine(string.Format("ERROR: File does not exists: {0}", filename));
                    continue;
                }

                var testsResultsViewerUrl = new Uri(ConfigurationManager.AppSettings["TestResultsViewerUrl"]);

                var client = new WebClient();
                var responseString = Encoding.Default.GetString(client.UploadFile(testsResultsViewerUrl, filename));

                JArray errors = null;
                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    JObject joResponse = JObject.Parse(responseString);
                    errors = (JArray)joResponse["Errors"];
                }

                string result = errors != null && errors.Count > 0
                    ? string.Format("ERROR: Couldn't upload file {0}", filename)
                    : string.Format("SUCCESS: File upload succeed {0}", filename);

                Console.WriteLine(result);
            }
        }
    }
}
