using System;
using System.Collections.Generic;
using System.Linq;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Parser
{
    public static class TestRunParseExtensions
    {
        public static T GetByType<T>(this object[] items)
        {
            if (items == null) return default(T);
            return ((T)items.Single(i => i.GetType() == typeof(T)));
        }

        public static List<UnitTestResult> GetUnitTestResults(this TestRunType testRunType)
        {
            return
                testRunType.Items.GetByType<ResultsType>()
                    .Items.Where(i => i.GetType() == typeof (UnitTestResultType))
                    .Select(i => ((UnitTestResultType) i).ToUnitTestResult())
                    .ToList();
        }

        public static UnitTestResult ToUnitTestResult(this UnitTestResultType unitTestResultType)
        {
            return new UnitTestResult
            {
                TestName = unitTestResultType.testName,
                Duration = unitTestResultType.duration != null ? TimeSpan.Parse(unitTestResultType.duration) : (TimeSpan?) null,
                StartTime = unitTestResultType.startTime != null ? DateTime.Parse(unitTestResultType.startTime) : (DateTime?) null,
                EndTime = unitTestResultType.endTime != null ? DateTime.Parse(unitTestResultType.endTime) : (DateTime?)null,
                Outcome = unitTestResultType.outcome,
                Output = unitTestResultType.Items.GetByType<OutputType>().ToOutput()
            };
        }

        public static Output ToOutput(this OutputType outputType)
        {
            if (null == outputType) return null;
            var result = new Output();

            if (outputType.StdOut != null)
            {
                result.StdOut = ((System.Xml.XmlNode[])outputType.StdOut)[0].Value;
            }

            if (outputType.ErrorInfo != null)
            {
                result.ErrorInfo = new ErrorInfo();

                
                if (outputType.ErrorInfo.Message != null)
                {
                    result.ErrorInfo.Message = ((System.Xml.XmlNode[])outputType.ErrorInfo.Message)[0].Value;
                }

                if (outputType.ErrorInfo.StackTrace != null)
                {
                    result.ErrorInfo.StackTrace = ((System.Xml.XmlNode[])outputType.ErrorInfo.StackTrace)[0].Value;
                }
            }

            return result;
        }

        public static Times ToTimes(this TestRunTypeTimes testRunTypeTimes)
        {
            return new Times
            {
                Creation = DateTime.Parse(testRunTypeTimes.creation),
                Queueing = DateTime.Parse(testRunTypeTimes.queuing),
                Start = DateTime.Parse(testRunTypeTimes.start),
                Finish = DateTime.Parse(testRunTypeTimes.finish)
            };
        }

        public static ResultSummary ToResultSummary(this TestRunTypeResultSummary testRunTypeResultSummary)
        {
            return new ResultSummary
            {
                Outcome = testRunTypeResultSummary.outcome,
                Counters = testRunTypeResultSummary.Items.GetByType<CountersType>().ToCounters()
            };
        }

        public static Counters ToCounters(this CountersType testRunCounters)
        {
            return new Counters
            {
                Total = testRunCounters.total,
                Executed = testRunCounters.executed,
                Passed = testRunCounters.passed,
                Error = testRunCounters.error,
                Timeout = testRunCounters.timeout,
                Aborted = testRunCounters.aborted,
                Inconclusive = testRunCounters.inconclusive,
                PassedButRunAborted = testRunCounters.passedButRunAborted,
                NotRunnable = testRunCounters.notRunnable,
                NotExecuted = testRunCounters.notExecuted,
                Disconnected = testRunCounters.disconnected,
                Warning = testRunCounters.warning,
                Completed = testRunCounters.completed,
                InProgress = testRunCounters.inProgress,
                Pending = testRunCounters.pending
            };
        }
    }
}