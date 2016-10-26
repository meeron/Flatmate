using Flatmate.Domain;
using Flatmate.Domain.Models;
using Flatmate.Domain.Repositories.Abstract;
using Flatmate.Web.Core;
using Flatmate.Web.Core.Security;
using Flatmate.Web.Infrastructure.Impl;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Web.Tests.Infrastructure
{
    public class AuthenticateServiceTests
    {
        private readonly AppConfig _config = new AppConfig { AuthenticationKey = "ANC86rK4e0Zekc11xcwtN1quvwgrrxX+wkxg8QJF768=" };
        private readonly IOptions<AppConfig> _options;

        public AuthenticateServiceTests()
        {
            var optionsMock = new Moq.Mock<IOptions<AppConfig>>();
            optionsMock.Setup(x => x.Value).Returns(_config);

            _options = optionsMock.Object;
        }

        [Fact(DisplayName = "GetToken_InvalidPassword")]
        public void GetToken_InvalidPassword()
        {
            var account = new Account { Email = "test" };
            account.SetPassword("test");

            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(account);

            var service = new AuthenticateService(moq.Object, _options);

            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = account.Email, Password = "invalid" });
            Assert.True(string.IsNullOrWhiteSpace(token));
        }

        [Fact(DisplayName = "GetToken_InvalidEmail")]
        public void GetToken_InvalidEmail()
        {
            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(() => null);

            var service = new AuthenticateService(moq.Object, _options);

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

            var service = new AuthenticateService(moq.Object, _options);

            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = account.Email, Password = "test" });
            Assert.False(string.IsNullOrWhiteSpace(token));
        }

        [Fact(DisplayName = "AuthenticateUserByToken_InvalidToken")]
        public void AuthenticateUserByToken_InvalidToken()
        {
            var service = new AuthenticateService(null, _options); //we don't new IAccountRepository instance at this stage
            var user = service.AuthenticateUserByToken(new AuthenticationToken(_options.Value.AuthenticationKeyBytes).Decrpt("asdasdasd"));

            Assert.Null(user);
        }

        [Fact(DisplayName = "AuthenticateUserByToken_ExpiredToken")]
        public void AuthenticateUserByToken_ExpiredToken()
        {
            var account = new Account { Email = "test" };
            account.SetPassword("test");

            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(account);

            var service = new AuthenticateService(moq.Object, _options);
            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = account.Email, Password = "test" });

            var authToken = new AuthenticationToken(_options.Value.AuthenticationKeyBytes);
            authToken.Decrpt(token);

            authToken.Expires = DateTime.Now.AddMinutes(-1);

            Assert.True(authToken.Timestamp > 0);

            var user = service.AuthenticateUserByToken(authToken);

            Assert.Null(user);
        }

        [Fact(DisplayName = "AuthenticateUserByToken")]
        public void AuthenticateUserByToken()
        {
            var account = new Account { Email = "test", Id = ModelId.NewId() };
            account.SetPassword("test");

            var moq = new Moq.Mock<IAccountRepository>();
            moq.Setup(x => x.FindByEmail("test")).Returns(account);

            var service = new AuthenticateService(moq.Object, _options);
            string token = service.GetToken(new Features.Api.Authenticate.PostViewModel { Email = account.Email, Password = "test" });

            var authToken = new AuthenticationToken(_options.Value.AuthenticationKeyBytes);
            authToken.Decrpt(token);

            Assert.True(authToken.Timestamp > 0);

            var user = service.AuthenticateUserByToken(authToken);

            Assert.NotNull(user);
            Assert.Equal(account.Email, user.Email);
            Assert.Equal(account.Id.ToString(), user.Id);
        }
    }
}
