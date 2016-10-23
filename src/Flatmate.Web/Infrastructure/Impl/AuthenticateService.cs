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

namespace Flatmate.Web.Infrastructure.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        private static byte[] IV = new byte[] { 86, 225, 88, 115, 210, 104, 117, 49, 129, 228, 169, 119, 171, 89, 240, 23 };

        private readonly IAccountRepository _accountRepo;
        private readonly AppConfig _config;

        public AuthenticateService(IAccountRepository accountRepo, IOptions<AppConfig> options)
        {
            _accountRepo = accountRepo;
            _config = options.Value;

            _config.ThrowIfInvalid();
        }

        public string GetToken(PostViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var account = _accountRepo.FindByEmail(model.Email);
            if (account == null)
                return string.Empty;

            if (account.ValidatePassword(model.Password))
            {
                //if password match, create token
                var tokenObject = new AuthenticationToken
                {
                    Email = model.Email,
                    Expires = DateTime.Now.AddMinutes(_config.AuthenticationTimeout),
                    Timestamp = DateTime.Now.Ticks
                };
                string tokenJson = JsonConvert.SerializeObject(tokenObject);
                byte[] tokenBytes = Encoding.UTF8.GetBytes(tokenJson);

                using (var aes = Aes.Create())
                {
                    aes.IV = IV;
                    aes.Key = Convert.FromBase64String(_config.AuthenticationKey);

                    return Convert.ToBase64String(aes.CreateEncryptor().TransformFinalBlock(tokenBytes, 0, tokenBytes.Length));
                }
            }

            return string.Empty;
        }
    }
}
