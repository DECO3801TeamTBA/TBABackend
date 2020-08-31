using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Shortlist
    {
        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }
        public Guid ShortListId { get; set; }
        public string ListName { get; set; }
    }
}
