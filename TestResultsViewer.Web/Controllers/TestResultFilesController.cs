using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestResultsViewer.Parser;
using TestResultsViewer.Parser.Entities;
using TestResultsViewer.Web.Models;

namespace TestResultsViewer.Web.Controllers
{
    public class TestResultFilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var outputDirectory = ConfigurationManager.AppSettings["ResultFilesOutputDirectory"];

            var httpContext = HttpContext.Current;

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (httpContext.Request.Files.Count < 1) return new HttpResponseMessage(HttpStatusCode.BadRequest);

                List<string> errors = new List<string>();

                foreach (var postedFile in httpContext.Request.Files.AllKeys.Select(key => httpContext.Request.Files[key]))
                {
                    var outputFilePath = Path.Combine(outputDirectory, postedFile.FileName);
                    postedFile.SaveAs(outputFilePath);

                    try
                    {
                        TestRun testRun;
                        using (var fileStream = new StreamReader(outputFilePath))
                        {
                            var parser = new ResultsParser();
                            testRun = parser.Parse(fileStream);
                        }


                        var newFilePath = Path.Combine(outputDirectory, string.Format("{0}.trx", testRun.Id));

                        if (File.Exists(newFilePath)) File.Delete(newFilePath);
                        File.Move(outputFilePath, newFilePath);
                    }
                    catch (Exception)
                    {
                        errors.Add(string.Format("Unable to save file {0}", postedFile.FileName));
                        File.Delete(outputFilePath);
                    }
                }

                if (errors.Count > 0)
                {
                    var uploadResult = new FileUploadResult { Created = DateTime.Now, Errors = errors };
                    return Request.CreateResponse(HttpStatusCode.Accepted, uploadResult);
                }

                return Request.CreateResponse(HttpStatusCode.Accepted);
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            var outputDirectory = ConfigurationManager.AppSettings["ResultFilesOutputDirectory"];

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                TestRunsQueryResult result = new TestRunsQueryResult();
                foreach (var file in Directory.GetFiles(outputDirectory, "*.trx", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        using (var fileStream = new StreamReader(file))
                        {
                            var parser = new ResultsParser();
                            result.TestRuns.Add(parser.Parse(fileStream));
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add(string.Format("Error occurred reading file {0}: {1}", file, ex.Message));
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            });
        }
    }
}
