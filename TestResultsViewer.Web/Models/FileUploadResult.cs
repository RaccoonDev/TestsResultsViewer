using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestResultsViewer.Web.Models
{
    public class FileUploadResult
    {
        public DateTime Created { get; set; }
        public List<string> Errors { get; set; }
    }
}