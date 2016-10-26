using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Flatmate.Web.Core.Security
{
    public class AuthenticationToken
    {
        private static byte[] IV = new byte[] { 86, 225, 88, 115, 210, 104, 117, 49, 129, 228, 169, 119, 171, 89, 240, 23 };

        private readonly byte[] _key;

        public AuthenticationToken()
        {
        }

        public AuthenticationToken(byte[] key)
        {
            _key = key;
        }

        public string Email { get; set; }
        public DateTime Expires { get; set; }
        public long Timestamp { get; set; }

        public string Encrypt()
        {
            string tokenJson = JsonConvert.SerializeObject(this);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(tokenJson);

            using (var aes = Aes.Create())
            {
                aes.IV = IV;
                aes.Key = _key;

                return Convert.ToBase64String(aes.CreateEncryptor().TransformFinalBlock(tokenBytes, 0, tokenBytes.Length));
            }
        }

        public AuthenticationToken Decrpt(string token)
        {
            //TODO: add log information why authentication failed

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("token");

            //reset this instance values
            Email = string.Empty;
            Expires = DateTime.MinValue;
            Timestamp = 0;

            using (var aes = Aes.Create())
            {
                aes.IV = IV;
                aes.Key = _key;

                using (var decryptor = aes.CreateDecryptor())
                {
                    string tokenJson;

                    try
                    {
                        byte[] tokenBytes = Convert.FromBase64String(token);
                        tokenJson = Encoding.UTF8.GetString(decryptor.TransformFinalBlock(tokenBytes, 0, tokenBytes.Length));
                    }
                    catch { return this; }

                    var authToken = JsonConvert.DeserializeObject<AuthenticationToken>(tokenJson);

                    Email = authToken.Email;
                    Expires = authToken.Expires;
                    Timestamp = authToken.Timestamp;
                    return this;
                }
            }
        }
    }
}
