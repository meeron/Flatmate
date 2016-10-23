using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flatmate.Web.Features.Api.Authenticate;
using Flatmate.Web.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Flatmate.Web.Features.Api
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]PostViewModel model)
        {
            string token = _authenticateService.GetToken(model);
            if (string.IsNullOrWhiteSpace(token))
                return StatusCode(401);

            return Json(token);
        }
    }
}
