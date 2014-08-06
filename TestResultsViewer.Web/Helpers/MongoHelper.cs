using MongoDB.Driver;
using TestResultsViewer.Parser.Entities;

namespace TestResultsViewer.Parser
{
    public class MongoHelper<T> where T : Entity
    {
        public MongoCollection<T> Collection { get; private set; }

        public MongoHelper()
        {
            var client = new MongoClient();
            var server = client.GetServer();
            var database = server.GetDatabase("TestResultViewer");
            Collection = database.GetCollection<T>(typeof (T).Name.ToLower());
        }
    }
}
