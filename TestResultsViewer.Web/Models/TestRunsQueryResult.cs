using System.Collections.Generic;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Web.Models
{
    public class TestRunsQueryResult
    {
        public TestRunsQueryResult()
        {
            TestRuns = new List<TestRun>();
            Errors = new List<string>();
        }

        public List<TestRun> TestRuns { get; set; }
        public List<string> Errors { get; set; }
    }
}