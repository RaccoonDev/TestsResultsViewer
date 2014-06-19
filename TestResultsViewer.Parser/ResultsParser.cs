using System;
using System.IO;
using System.Xml.Serialization;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Parser
{
    public class ResultsParser
    {
        public TestRun Parse(StreamReader streamReader)
        {
            var serializer = new XmlSerializer(typeof(TestRunType));
            var testRunType = (TestRunType)serializer.Deserialize(streamReader);

            var result = new TestRun
            {
                Id = Guid.Parse(testRunType.id),
                Times = testRunType.Items.GetByType<TestRunTypeTimes>().ToTimes(),
                ResultSummary = testRunType.Items.GetByType<TestRunTypeResultSummary>().ToResultSummary(),
                UnitTestResults = testRunType.GetUnitTestResults()
            };

            return result;
        }
    }
}
