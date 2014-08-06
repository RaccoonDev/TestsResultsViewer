using System;
using System.IO;
using System.Xml.Serialization;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Parser
{
    public class ResultsParser
    {
        public TestRun Parse(StreamReader streamReader, string buildName)
        {
            var serializer = new XmlSerializer(typeof(TestRunType));
            var deserializedTestRun = (TestRunType)serializer.Deserialize(streamReader);

            var result = new TestRun
            {
                Id = Guid.Parse(deserializedTestRun.id),
                Name = deserializedTestRun.name,
                Times = deserializedTestRun.Items.GetByType<TestRunTypeTimes>().ToTimes(),
                ResultSummary = deserializedTestRun.Items.GetByType<TestRunTypeResultSummary>().ToResultSummary(),
                UnitTestResults = deserializedTestRun.GetUnitTestResults(),
                BuildName = buildName
            };

            return result;
        }
    }
}
