using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flatmate.Web.Features.Api.Authenticate;
using Flatmate.Domain.Repositories.Abstract;
using Microsoft.Extensions.Options;
using Flatmate.Web.Core;
using Flatmate.Web.Core.Security;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using Flatmate.Domain.Models;

namespace Flatmate.Web.Infrastructure.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly AppConfig _config;

        public AuthenticateService(IAccountRepository accountRepo, IOptions<AppConfig> options)
        {
            _accountRepo = accountRepo;
            _config = options.Value;

            _config.ThrowIfInvalid();
        }

        public AuthenticatedUser AuthenticateUserByToken(AuthenticationToken token)
        {
            //TODO: log info why authentication failed.

            if (token == null)
                throw new ArgumentNullException("token");

            if (token.Timestamp <= 0)
                return null; //invalid token

            if (DateTime.Now > token.Expires)
                return null; //token expired

            Account account = _accountRepo.FindByEmail(token.Email);
            if (account == null)
                throw new InvalidOperationException(string.Format("Cannot find account '{0}'.", token.Email)); //something is wrong when valid token contains invalid user

            return AuthenticatedUser.FromAccount(account);
        }

        public string GetToken(PostViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            Account account = _accountRepo.FindByEmail(model.Email);
            if (account == null)
                return string.Empty;

            if (account.ValidatePassword(model.Password))
            {
                //if password match, create token
                var tokenObject = new AuthenticationToken(_config.AuthenticationKeyBytes);

                tokenObject.Email = model.Email;
                tokenObject.Expires = DateTime.Now.AddMinutes(_config.AuthenticationTimeout);
                tokenObject.Timestamp = DateTime.Now.Ticks;

                return tokenObject.Encrypt();
            }

            return string.Empty;
        }
    }
}
