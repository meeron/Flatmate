using Flatmate.Domain.Models;
using Flatmate.Domain.Tests.Mock.Mongo;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain.Tests.Mock
{
    public static class MockHelper
    {
        public static IMongoDatabase CreateDatabaseForCollection<T>(bool useRealDb = false)
            where T: class, new()
        {
            if (useRealDb)
            {
                var client = new MongoClient("mongodb://localhost:27017");
                return client.GetDatabase("unit_test_db");
            }

            var mockdb = new Mock<IMongoDatabase>();
            mockdb.Setup(x => x.GetCollection<T>(typeof(T).Name, null)).Returns(new FakeMongoCollection<T>());
            return mockdb.Object;
        }
    }
}
