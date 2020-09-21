using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Activity
    {
        // Table Properties
        [ForeignKey("Content")]
        public Guid ActivityId { get; set; }

        // Navigation Properties i.e FKs to different tables
        public Content Content { get; set; }
    }
}
