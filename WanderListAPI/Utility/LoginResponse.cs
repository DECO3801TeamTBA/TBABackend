using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;

namespace WanderListAPI.Utility
{
    /// <summary>
    /// Simple POCO for response to API requests related to successful logins
    /// </summary>
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public ResponseUser User { get; set; }
    }
}
