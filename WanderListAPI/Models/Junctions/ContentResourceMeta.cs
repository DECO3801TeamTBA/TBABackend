using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models.Junctions
{
    public class ContentResourceMeta
    {
        [ForeignKey("Content")]
        public Guid ContentId { get; set; }
        [ForeignKey("ResourceMeta")]
        public Guid ResourceMetaId { get; set; }
        public int Number { get; set; }

        //Navigation properties
        public Content Content { get; set; }
        public ResourceMeta ResourceMeta { get; set; }
    }
}
