using Flatmate.Domain.Enums;
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
        #region Constructors
        public Account()
        {
            FacebookIds = new HashSet<Facebook>();
            UserProfile = new Profile();
        }
        #endregion

        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public Profile UserProfile { get; set; }

        public ICollection<Facebook> FacebookIds { get; set; }
        #endregion

        #region Methods
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
        #endregion

        #region Classes
        public class Facebook
        {
            public string AppId { get; set; }
            public int UserId { get; set; }
        }

        public class Profile
        {
            public string PhoneNumber { get; set; }
            public string Name { get; set; }
            public bool ReceiveSmsNotifications { get; set; }
        }
        #endregion
    }
}
