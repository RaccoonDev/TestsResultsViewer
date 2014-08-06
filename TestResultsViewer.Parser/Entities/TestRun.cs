using System.Collections.Generic;

namespace TestResultsViewer.Parser.Entities
{
    public class TestRun : Entity
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public Times Times { get; set; }
        public ResultSummary ResultSummary { get; set; }
        public List<UnitTestResult> UnitTestResults { get; set; }
        public string BuildName { get; set; }
    }
}