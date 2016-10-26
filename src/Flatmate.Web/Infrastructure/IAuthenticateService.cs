using Flatmate.Web.Core.Security;
using Flatmate.Web.Features.Api.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Web.Infrastructure
{
    public interface IAuthenticateService
    {
        string GetToken(PostViewModel model);
        AuthenticatedUser AuthenticateUserByToken(AuthenticationToken token);
    }
}
