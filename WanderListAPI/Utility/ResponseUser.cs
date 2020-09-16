using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Utility
{
    /// <summary>
    /// Simple POCO for response to API requests related to successful logins
    /// </summary>
    public class ResponseUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Points { get; set; }
        public string UserName { get; set; }
    }
}

