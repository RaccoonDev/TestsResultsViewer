using System;
using System.Collections.Generic;
using System.IO;
using TestResultsViewer.Parser;
using TestResultsViewer.Parser.Entities;
using TestResultsViewer.Parser.Interfaces;

namespace TestResultsViewer.Web.Storages
{
    public class FileSystemResultsStorage : IResultsStorage
    {
        private readonly string _outputDirectory;

        public FileSystemResultsStorage(string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
                throw new ArgumentException(string.Format("Specified directory must exist: {0}", outputDirectory));

            _outputDirectory = outputDirectory;
        }

        public void Store(Stream inputStream, string buildName = "")
        {
            using (var fileStream = new StreamReader(inputStream))
            {
                var parser = new ResultsParser();
                TestRun testRun = parser.Parse(fileStream, buildName);

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
            foreach (var file in Directory.GetFiles(_outputDirectory, "*.trx", SearchOption.TopDirectoryOnly))
            {
                using (var fileStream = new StreamReader(file))
                {
                    var parser = new ResultsParser();
                    var testRun = parser.Parse(fileStream, string.Empty);
                    testRun.FileName = Path.GetFileName(file);
                    yield return testRun;
                }
            }
        }

        public Stream GetOriginalContentById(string filename)
        {
            return new FileStream(Path.Combine(_outputDirectory, filename), FileMode.Open);
        }

        public void DeleteAll()
        {
            var directoryInfo = new DirectoryInfo(_outputDirectory);
            foreach (var file in directoryInfo.GetFiles("*.trx"))
            {
                file.Delete();
            }
        }
    }
}
