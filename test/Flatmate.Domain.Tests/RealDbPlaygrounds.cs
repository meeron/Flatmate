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

            var accountRepository = new AccountRepository(client.GetDatabase("flatmate_playground"));
            accountRepository.Insert(new Account { Email = "test", Password = "pass1" });

            var acc = new Account();
            acc.FacebookIds.Add(new Account.Facebook { AppId = "Test", UserId = 12312321 });

            accountRepository.Insert(acc);

            var dbacc = accountRepository.FindById(acc.Id);

            Assert.NotNull(dbacc);
        }
    }
}
