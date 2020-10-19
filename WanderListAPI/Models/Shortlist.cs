using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Shortlist
    {
        //Table properties
        public Guid ShortlistId { get; set; }
        public string ListName { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        //Navigation properties
        public AppUser AppUser { get; set; }
    }
}
