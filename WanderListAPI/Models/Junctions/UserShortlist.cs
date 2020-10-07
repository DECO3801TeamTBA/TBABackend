using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models.Junctions
{
    public class UserShortlist
    {
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("Shortlist")]
        public Guid ShortlistId { get; set; }

        //Navigation properties
        public AppUser AppUser { get; set; }
        public Shortlist Shortlist { get; set; }
    }
}
