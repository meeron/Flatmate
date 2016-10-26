using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Web.Core
{
    public class AppConfig
    {
        private const int DEF_TIMEOUT = 30;

        /// <summary>
        /// AES 32bit key in Base64 format
        /// </summary>
        public string AuthenticationKey { get; set; }

        /// <summary>
        /// Indicates in minutes how log generated authentication token is valid
        /// </summary>
        public int AuthenticationTimeout { get; set; }

        public void ThrowIfInvalid()
        {
            if (string.IsNullOrWhiteSpace(AuthenticationKey))
                throw new Exception("Configuration: 'AuthenticationKey' cannot be empty.");

            if (AuthenticationTimeout <= 0)
                AuthenticationTimeout = DEF_TIMEOUT;
        }

        public byte[] AuthenticationKeyBytes
        {
            get { return Convert.FromBase64String(AuthenticationKey); }
        }
    }
}
