using System.Collections.Generic;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Web.Models
{
    public class TestRunsQueryResult
    {
        public TestRunsQueryResult()
        {
            TestRuns = new List<TestRun>();
            Builds = new List<string>();
        }

        public List<TestRun> TestRuns { get; set; }
        public List<string> Builds { get; set; } 
    }
}