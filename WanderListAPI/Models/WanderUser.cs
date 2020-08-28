using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    /// <summary>
    /// WanderList user specific details should go here
    /// </summary>
    public class WanderUser
    {
        //Navigation properties
        public ICollection<History> Histories { get; set; }
    }
}
