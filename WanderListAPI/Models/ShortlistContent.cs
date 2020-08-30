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
        public Guid ListId { get; set; }
        [ForeignKey("Content")]
        public Guid ContentId { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
