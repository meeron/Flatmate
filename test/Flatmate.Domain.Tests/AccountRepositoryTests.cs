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
using Flatmate.Domain;

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
            Assert.Equal(0, accountRepository.Find(x => x.UserProfile.Name == "test").Count());
        }

        [Fact]
        public void Find_By_Id()
        {
            var id = ModelId.NewId();

            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());
            accountRepository.Insert(new Account { Id = id });

            Assert.NotNull(accountRepository.FindById(id));
        }

        [Fact]
        public void Count_Expresson()
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());

            accountRepository.Insert(new Account { Email = "test", Password = "pass1" });
            accountRepository.Insert(new Account { Email = "test", Password = "pass2" });

            Assert.Equal(2, accountRepository.Count(x => x.Email == "test"));
        }

        [Fact]
        public void Count()
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());

            accountRepository.Insert(new Account { Email = "test", Password = "pass1" });
            accountRepository.Insert(new Account { Email = "test", Password = "pass2" });

            Assert.Equal(2, accountRepository.Count());
        }

        [Fact]
        public void Insert()
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());

            var account = new Account { Email = "test" };
            accountRepository.Insert(account);

            Assert.NotNull(accountRepository.Find(x => x.Email == "test").SingleOrDefault());
        }

        [Fact]
        public void Update_By_Object()
        {
            string newEmail = "new_test_email";

            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());
            var account = new Account { Id = ModelId.NewId(), Email = "test" };
            accountRepository.Insert(account);

            account.Email = newEmail;

            accountRepository.Update(account);

            var updAccount = accountRepository.FindById(account.Id);

            Assert.Equal(newEmail, updAccount.Email);
        }

        [Fact]
        public void Update_By_Fields()
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());

            accountRepository.Insert(new Account { Email = "test", Password = "pass1" });
            accountRepository.Insert(new Account { Email = "test", Password = "pass2" });

            long countAll = accountRepository.Find().Count();

            var updateDef = Update<Account>.Set("Password", "pass3")
                                .And("Name", "name_test");

            long countUpd = accountRepository.UpdateWhere(x => x.Email == "test", updateDef);

            Assert.Equal(countAll, countUpd);
        }

        [Fact]
        public void Delete_Where()
        {
            var accountRepository = new AccountRepository(MockHelper.CreateDatabaseForCollection<Account>());

            accountRepository.Insert(new Account { Email = "test", Password = "pass1" });
            accountRepository.Insert(new Account { Email = "test", Password = "pass2" });

            long deleteCount = accountRepository.DeleteWhere(x => x.Email == "test");
            Assert.Equal(2, deleteCount);
        }
    }
}
