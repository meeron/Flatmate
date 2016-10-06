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
        public static IMongoDatabase CreateDatabaseForCollection<T>()
            where T: class, new()
        {
            var mockdb = new Mock<IMongoDatabase>();
            mockdb.Setup(x => x.GetCollection<Account>(typeof(T).Name, null)).Returns(new FakeMongoCollection<Account>());
            return mockdb.Object;
        }
    }
}
