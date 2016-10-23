using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flatmate.Web.Features.Api.Authenticate;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Flatmate.Web.Features.Api
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        [HttpPost]
        public string Post([FromBody]PostViewModel model)
        {
            return "OK";
        }
    }
}
