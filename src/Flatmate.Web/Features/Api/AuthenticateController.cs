using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flatmate.Web.Features.Api.Authenticate;
using Flatmate.Web.Infrastructure;
using System.Net;
using Flatmate.Web.Core.Security;
using Flatmate.Web.Core;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Flatmate.Web.Features.Api
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly AppConfig _config;

        public AuthenticateController(IAuthenticateService authenticateService, IOptions<AppConfig> options)
        {
            _authenticateService = authenticateService;
            _config = options.Value;
        }

        [HttpGet, Route("{token}")]
        public IActionResult Get(string token)
        {
            var authToken = new AuthenticationToken(_config.AuthenticationKeyBytes);
            
            AuthenticatedUser user = _authenticateService.AuthenticateUserByToken(authToken.Decrpt(token));
            if (user == null)
                return StatusCode((int)HttpStatusCode.Unauthorized);

            return Json(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PostViewModel model)
        {
            string token = _authenticateService.GetToken(model);
            if (string.IsNullOrWhiteSpace(token))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            return Json(token);
        }
    }
}
