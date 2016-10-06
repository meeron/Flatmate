using Flatmate.Domain.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Flatmate.Domain.Models
{
    public class Account: ModelBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public bool ValidatePassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                string hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return Password.Equals(hash);
            }
        }

        public void SetPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                Password = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
