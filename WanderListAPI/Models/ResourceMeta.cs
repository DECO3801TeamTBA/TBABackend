using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class ResourceMeta
    {
        // Table Properties
        [ForeignKey("Content")]
        public Guid ResourceMetaId { get; set; }
        public Resource Resource { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string MimeType { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public long Length { get; set; }
        public DateTime? AddedOn { get; set; }
        public string Description { get; set; }

        //Maybe this won't be needed but we may need to have both
        //disk and db stored files
        public bool OnDisk { get; set; }
    }
}
