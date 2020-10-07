using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class QR
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QRId { get; set; }
        public DateTime Expirey { get; set; }
        [ForeignKey("ResourceMeta")]
        public Guid ContentId { get; set; }

        // Navigation Properties
        public Content Content { get; set; }
    }
}
