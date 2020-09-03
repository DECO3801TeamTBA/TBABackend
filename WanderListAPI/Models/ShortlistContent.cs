using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class ShortlistContent
    {
        [ForeignKey("Shortlist")]
        public Guid ShortlistId { get; set; }
        [ForeignKey("Content")]
        public Guid ContentId { get; set; }
        [Required]
        public int Number { get; set; }


        //Navigation properties
        public Shortlist Shortlist { get; set; }
        public Content Content { get; set; }
        
    }
}
