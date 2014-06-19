using System;

namespace TestResultsViewer.Parser.Entities
{
    public class Times
    {
        public DateTime Creation { get; set; }
        public DateTime Queueing { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}