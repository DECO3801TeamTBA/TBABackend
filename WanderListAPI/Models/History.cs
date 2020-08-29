using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderListAPI.Models
{
    public class History
    {   
        [ForeignKey("Content")]
        public Guid ContentId { get; set; }
        [ForeignKey("WanderUser")]
        public string UserId { get; set; }
        
        public DateTime Date { get; set; }

        //Navigation Properties
        public virtual Content Content { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
