using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Destination
    {
        // Table Properties
        [ForeignKey("Content")]
        public Guid DestinationId { get; set; }

        // Navigation Properties i.e FKs to different tables
        public Content Content { get; set; }
    }
}
