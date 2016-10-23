using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain
{
    public static class MongoDatabaseFactory
    {
        public static IMongoDatabase Create(string connectionString)
        {
            string databaseName = MongoUrl.Create(connectionString).DatabaseName;

            var client = new MongoClient(connectionString);
            return client.GetDatabase(databaseName);
        }
    }
}
