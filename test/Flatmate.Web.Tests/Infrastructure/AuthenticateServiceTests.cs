using Flatmate.Domain.Models;
using Flatmate.Domain.Repositories.Abstract;
using Flatmate.Web.Infrastructure.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Web.Tests.Infrastructure
{
    public class AuthenticateServiceTests
    {
        [Fact(DisplayName = "GetToken_InvalidPassword")]
        public void GetToken_InvalidPassword()
        {
            var account = new Account { Email = "test" };
            account.SetPassword("test");

            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(account);

            var service = new AuthenticateService(moq.Object);

            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = account.Email, Password = "invalid" });
            Assert.True(string.IsNullOrWhiteSpace(token));
        }

        [Fact(DisplayName = "GetToken_InvalidEmail")]
        public void GetToken_InvalidEmail()
        {
            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(() => null);

            var service = new AuthenticateService(moq.Object);

            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = "test", Password = "invalid" });
            Assert.True(string.IsNullOrWhiteSpace(token));
        }

        [Fact(DisplayName = "GetToken")]
        public void GetToken()
        {
            var account = new Account { Email = "test" };
            account.SetPassword("test");

            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(account);

            var service = new AuthenticateService(moq.Object);

            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = account.Email, Password = "test" });
            Assert.True(string.IsNullOrWhiteSpace(token));
        }
    }
}
