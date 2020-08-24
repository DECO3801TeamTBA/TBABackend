using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Content
    {
        //Properties for Content table columns
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public int? Capacity { get; set; }

        //Navigation Properties i.e FKs to different tables
        public ICollection<ResourceMeta> ResourceMetas { get; set; }
    }
}
