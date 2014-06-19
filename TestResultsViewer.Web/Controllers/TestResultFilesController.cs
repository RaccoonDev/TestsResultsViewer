using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestResultsViewer.Parser;

namespace TestResultsViewer.Web.Controllers
{
    public class TestResultFilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            var httpContext = HttpContext.Current;

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                if (httpContext.Request.Files.Count < 1) return new HttpResponseMessage(HttpStatusCode.BadRequest);

                string serverFolderPath = httpContext.Server.MapPath("~/");

                foreach (var postedFile in httpContext.Request.Files.AllKeys.Select(key => httpContext.Request.Files[key]))
                {
                    postedFile.SaveAs(Path.Combine(serverFolderPath, postedFile.FileName));
                }

                return new HttpResponseMessage(HttpStatusCode.Accepted);
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            var fileName = HttpContext.Current.Server.MapPath("~/TestResultsFiles/test2.trx");

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                var fileStream = new StreamReader(fileName);
                var parser = new ResultsParser();
                var result = parser.Parse(fileStream);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            });
        }
    }
}
