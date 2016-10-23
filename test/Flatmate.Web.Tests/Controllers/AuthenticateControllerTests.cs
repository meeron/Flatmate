using Flatmate.Web.Features.Api;
using Flatmate.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Web.Tests.Controllers
{
    public class AuthenticateControllerTests
    {
        [Fact(DisplayName = "Post_UserAuthenticationOk")]
        public void Post_UserAuthenticationOk()
        {
            string testToken = "test_token";

            var postViewModel = new Web.Features.Api.Authenticate.PostViewModel();

            var authenticateServiceMock = new Moq.Mock<IAuthenticateService>();
            authenticateServiceMock.Setup(x => x.GetToken(postViewModel)).Returns(testToken);

            var controller = new AuthenticateController(authenticateServiceMock.Object);
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

            var controller = new AuthenticateController(authenticateServiceMock.Object);
            IActionResult result = controller.Post(postViewModel);

            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(401, statusCodeResult.StatusCode);
        }
    }
}
