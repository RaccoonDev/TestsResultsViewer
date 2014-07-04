using System;
using System.Collections.Generic;

namespace TestResultsViewer.Parser.Entities
{
    public class TestRun
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public Times Times { get; set; }
        public ResultSummary ResultSummary { get; set; }
        public List<UnitTestResult> UnitTestResults { get; set; }
    }
}