using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Web.Core.Security
{
    public class AuthenticationToken
    {
        public string Email { get; set; }
        public DateTime Expires { get; set; }
        public long Timestamp { get; set; }
    }
}
