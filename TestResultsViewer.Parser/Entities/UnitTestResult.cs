using System;

namespace TestResultsViewer.Parser.Entities
{
    public class UnitTestResult
    {
        public string TestName { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Outcome { get; set; }
        public Output Output { get; set; }
    }
}