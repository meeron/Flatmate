using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flatmate.Web.Features.Api.Authenticate;
using Flatmate.Domain.Repositories.Abstract;

namespace Flatmate.Web.Infrastructure.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAccountRepository _accountRepo;

        public AuthenticateService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public string GetToken(PostViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var account = _accountRepo.FindByEmail(model.Email);
            if (account == null)
                return string.Empty;

            if (account.ValidatePassword(model.Password))
                throw new NotImplementedException();

            return string.Empty;
        }
    }
}
