using System.Collections.Generic;
using System.IO;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Parser.Interfaces
{
    public interface IResultsStorage
    {
        void Store(Stream inputStream, string buildName);
        IEnumerable<TestRun> GetAllRuns();
        Stream GetOriginalContentById(string filename);
        void DeleteAll();
        IEnumerable<string> GetBuildNames();
    }
}