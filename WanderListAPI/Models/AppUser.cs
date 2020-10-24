using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderListAPI.Models
{
    /// <summary>
    /// By inheriting from IdentityUser, we can use Microsoft Identity to handle
    /// passwords i.e hashing/salts/logins etc...
    /// </summary>
    public class AppUser : IdentityUser
    {
        // Table Properties
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Points { get; set; }

        [ForeignKey("ResourceMeta")]
        public Guid ProfilePicResourceMetaId { get; set; }
        //Navigation properties
        public ResourceMeta ProfilePic { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
