using Flatmate.Web.Core;
using Flatmate.Web.Core.Security;
using Flatmate.Web.Features.Api;
using Flatmate.Web.Features.Api.Authenticate;
using Flatmate.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Web.Tests.Controllers
{
    public class AuthenticateControllerTests
    {
        private readonly AppConfig _config = new AppConfig { AuthenticationKey = "ANC86rK4e0Zekc11xcwtN1quvwgrrxX+wkxg8QJF768=" };
        private readonly IOptions<AppConfig> _options;

        public AuthenticateControllerTests()
        {
            var optionsMock = new Moq.Mock<IOptions<AppConfig>>();
            optionsMock.Setup(x => x.Value).Returns(_config);

            _options = optionsMock.Object;
        }

        [Fact(DisplayName = "Post_UserAuthenticationOk")]
        public void Post_UserAuthenticationOk()
        {
            string testToken = "test_token";

            var postViewModel = new Web.Features.Api.Authenticate.PostViewModel();

            var authenticateServiceMock = new Moq.Mock<IAuthenticateService>();
            authenticateServiceMock.Setup(x => x.GetToken(postViewModel)).Returns(testToken);

            var controller = new AuthenticateController(authenticateServiceMock.Object, _options);
            IActionResult result = controller.Post(postViewModel);

            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(testToken, jsonResult.Value);
        }

        [Fact(DisplayName = "Post_UserAuthenticationFailed")]
        public void Post_UserAuthenticationFailed()
        {
            var postViewModel = new Web.Features.Api.Authenticate.PostViewModel();

            var authenticateServiceMock = new Moq.Mock<IAuthenticateService>();
            authenticateServiceMock.Setup(x => x.GetToken(postViewModel)).Returns(string.Empty);

            var controller = new AuthenticateController(authenticateServiceMock.Object, _options);
            IActionResult result = controller.Post(postViewModel);

            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(401, statusCodeResult.StatusCode);
        }

        [Fact(DisplayName = "Get_AuthenticationFailed")]
        public void Get_AuthenticationFailed()
        {
            var authToken = new AuthenticationToken();

            var authenticateServiceMock = new Moq.Mock<IAuthenticateService>();
            authenticateServiceMock.Setup(x => x.AuthenticateUserByToken(authToken)).Returns(() => null);

            var controller = new AuthenticateController(authenticateServiceMock.Object, _options);
            IActionResult result = controller.Get("some_invalid_token");

            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(401, statusCodeResult.StatusCode);
        }

        [Fact(DisplayName = "Get_AuthenticationOk")]
        public void Get_AuthenticationOk()
        {
            var authenticatedUser = new AuthenticatedUser { Email = "test" };

            var controller = new AuthenticateController(new FakeAuthService(authenticatedUser), _options);
            IActionResult result = controller.Get("test_token");

            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(authenticatedUser, jsonResult.Value);
        }

        private class FakeAuthService : IAuthenticateService
        {
            private readonly AuthenticatedUser _user;

            public FakeAuthService(AuthenticatedUser user)
            {
                _user = user;
            }

            public AuthenticatedUser AuthenticateUserByToken(AuthenticationToken token)
            {
                return _user;
            }

            public string GetToken(PostViewModel model)
            {
                throw new NotImplementedException();
            }
        }
    }
}
