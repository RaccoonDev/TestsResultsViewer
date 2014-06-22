using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestResultsViewer.Parser.Interfaces;
using TestResultsViewer.Web.Models;

namespace TestResultsViewer.Web.Controllers
{
    public class TestResultFilesController : ApiController
    {
        private readonly IResultsStorage _resultsStorage;

        public TestResultFilesController(IResultsStorage resultsStorage)
        {
            _resultsStorage = resultsStorage;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var httpContext = HttpContext.Current;

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (httpContext.Request.Files.Count < 1) return new HttpResponseMessage(HttpStatusCode.BadRequest);

                HttpPostedFile postedFile = httpContext.Request.Files.AllKeys.Select(key => httpContext.Request.Files[key]).First();

                try
                {
                    _resultsStorage.Store(postedFile.InputStream);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

                return Request.CreateResponse(HttpStatusCode.Accepted);
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            return await Task<HttpResponseMessage>.Factory
                .StartNew(() => 
                    Request.CreateResponse(HttpStatusCode.OK, new TestRunsQueryResult 
                    {
                        TestRuns = _resultsStorage.GetAllRuns().ToList()
                    }
            ));
        }
    }
}
