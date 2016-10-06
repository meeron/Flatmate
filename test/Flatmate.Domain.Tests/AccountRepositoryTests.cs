using Flatmate.Domain.Models;
using Flatmate.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using System.Collections;
using System.Linq.Expressions;
using Flatmate.Domain.Tests.Mock;

namespace Tests
{
    public class AccountRepository_Tests
    {
        [Fact]
        public void Find() 
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());
            Assert.Equal(0, accountRepository.Find().Count());
        }

        [Fact]
        public void Find_Expression()
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());
            Assert.Equal(0, accountRepository.Find(x => x.Name == "test").Count());
        }
    }
}
