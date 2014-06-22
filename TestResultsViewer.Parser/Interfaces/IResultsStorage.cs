using System;
using System.Collections.Generic;
using System.IO;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Parser.Interfaces
{
    public interface IResultsStorage
    {
        void Store(Stream inputStream);
        IEnumerable<TestRun> GetAllRuns();
        FileStream GetOriginalContentById(Guid id);
    }
}