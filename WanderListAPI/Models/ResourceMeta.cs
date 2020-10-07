using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace WanderListAPI.Models
{
    public class ResourceMeta
    {
        // Table Properties
        [ForeignKey("Resource")]
        public Guid ResourceMetaId { get; set; }
        //  Naviagtion property 1:1 relationship, goes here before we lookup resourceMeta to find
        //  actual resources
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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
