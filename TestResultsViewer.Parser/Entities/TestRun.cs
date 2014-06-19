using System;
using System.Collections.Generic;

namespace TestResultsViewer.Parser.Entities
{
    public class TestRun
    {
        public Guid Id { get; set; }
        public Times Times { get; set; }
        public ResultSummary ResultSummary { get; set; }
        public List<UnitTestResult> UnitTestResults { get; set; }
    }
}