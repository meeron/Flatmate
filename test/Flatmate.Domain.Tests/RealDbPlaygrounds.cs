using Flatmate.Domain.Models;
using Flatmate.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Domain.Tests
{
    public class RealDbPlaygrounds
    {
        [Fact]
        public void RealDb_Playground()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var accountRepository = new AccountRepository(client.GetDatabase("Flatmate"));

            var acc = new Account { Email = "admin@flatmate.io" };        
            acc.SetPassword("xxx");

            accountRepository.Insert(acc);

            var dbacc = accountRepository.FindById(acc.Id);

            Assert.NotNull(dbacc);
        }
    }
}
