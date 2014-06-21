using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestResultsViewer.Tests
{
    [TestClass]
    public class ResultsParserTest
    {
        [TestMethod]
        public void TypeRunSerializerCanBeCreated()
        {
            var serializer = new XmlSerializer(typeof(TestRunType));
            Assert.IsNotNull(serializer);
        }

        [TestMethod]
        public void SomeAnotherTestToPass()
        {
            
        }
    }
}
