using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flatmate.Domain.Models;

namespace Flatmate.Web.Features.Api.Authenticate
{
    public class AuthenticatedUser
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public static AuthenticatedUser FromAccount(Account account)
        {
            return new AuthenticatedUser
            {
                Id = account.Id.ToString(),
                Email = account.Email
            };
        }
    }
}
