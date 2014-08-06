using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver.Linq;
using TestResultsViewer.Parser;
using TestResultsViewer.Parser.Entities;
using TestResultsViewer.Parser.Interfaces;

namespace TestResultsViewer.Web.Storages
{
    public class MongoResultStorage : IResultsStorage
    {
        private readonly string _outputDirectory;
        private readonly MongoHelper<TestRun> _testRunHelper;

        public MongoResultStorage(string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
                throw new ArgumentException(string.Format("Specified directory must exist: {0}", outputDirectory));

            _outputDirectory = outputDirectory;

            _testRunHelper = new MongoHelper<TestRun>();
        }

        public void Store(Stream inputStream, string buildName)
        {
            using (var fileStream = new StreamReader(inputStream))
            {
                var parser = new ResultsParser();
                TestRun testRun = parser.Parse(fileStream, buildName);
                _testRunHelper.Collection.Save(testRun);

                var newFilePath = Path.Combine(_outputDirectory, string.Format("{0}.trx", testRun.Id));

                if (File.Exists(newFilePath)) File.Delete(newFilePath);
                using (var newFileStream = File.Create(newFilePath))
                {
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.CopyTo(newFileStream);
                }
            }
        }

        public IEnumerable<TestRun> GetAllRuns()
        {
            var result = _testRunHelper.Collection.AsQueryable().ToArray();
            return result;
        }

        public Stream GetOriginalContentById(string filename)
        {
            return new FileStream(Path.Combine(_outputDirectory, filename), FileMode.Open);
        }

        public void DeleteAll()
        {
            _testRunHelper.Collection.RemoveAll();
            var directoryInfo = new DirectoryInfo(_outputDirectory);
            foreach (var file in directoryInfo.GetFiles("*.trx"))
            {
                file.Delete();
            }
        }
    }
}
