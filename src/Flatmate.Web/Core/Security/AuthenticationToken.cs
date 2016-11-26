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
        private readonly byte[] _key;

        public AuthenticationToken()
        {
        }

        public AuthenticationToken(byte[] key)
        {
            using(var sha256 = SHA256.Create())
            {
                //compute hash to get 32bit key
                _key = sha256.ComputeHash(key);
            }
        }

        public string Email { get; set; }
        public DateTime Expires { get; set; }
        public long Timestamp { get; set; }

        public string Encrypt()
        {
            string tokenJson = JsonConvert.SerializeObject(this);
            byte[] tokenObjBytes = Encoding.UTF8.GetBytes(tokenJson);

            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                byte[] encData = aes.CreateEncryptor().TransformFinalBlock(tokenObjBytes, 0, tokenObjBytes.Length);
                byte[] tokenBytes = aes.IV.Concat(encData).ToArray();
                string token = Convert.ToBase64String(tokenBytes);
                
                //replace '/' so we could use token in URI
                return token.Replace('/','_');
            }
        }

        public AuthenticationToken Decrpt(string token)
        {
            //TODO: add log information why authentication failed

            token = token.Replace('_','/');

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("token");

            //reset this instance values
            Email = string.Empty;
            Expires = DateTime.MinValue;
            Timestamp = 0;
            
            byte[] tokenBytes = null;
            
            try 
            {
                tokenBytes = Convert.FromBase64String(token);
            } catch { return this; }

            using (var aes = Aes.Create())
            {
                byte[] iv = new byte[aes.IV.Length];
                byte[] tokenObjBytes = new byte[tokenBytes.Length - iv.Length];            

                Array.Copy(tokenBytes, iv, iv.Length);
                Array.Copy(tokenBytes, iv.Length, tokenObjBytes, 0, tokenObjBytes.Length);

                aes.IV = iv;
                aes.Key = _key;

                using (var decryptor = aes.CreateDecryptor())
                {
                    string tokenJson;

                    try
                    {
                        byte[] decJson = decryptor.TransformFinalBlock(tokenObjBytes, 0, tokenObjBytes.Length);
                        tokenJson = Encoding.UTF8.GetString(decJson);

                        var authToken = JsonConvert.DeserializeObject<AuthenticationToken>(tokenJson);

                        Email = authToken.Email;
                        Expires = authToken.Expires;
                        Timestamp = authToken.Timestamp;
                        return this;
                    }
                    catch { return this; }

                }
            }
        }
    }
}
