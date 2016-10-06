using Flatmate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Domain.Tests.Models
{
    public class AccountTests
    {
        [Fact]
        public void SetPassword_True()
        {
            string pass = "test_pass";

            var account = new Account();
            account.SetPassword(pass);

            Assert.True(account.ValidatePassword(pass));
        }

        [Fact]
        public void SetPassword_False()
        {
            string pass = "test_pass";

            var account = new Account();
            account.SetPassword(pass);

            Assert.False(account.ValidatePassword("pass_test"));
        }
    }
}
