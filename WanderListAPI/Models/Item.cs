using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("ResourceMeta")]
        public Guid CoverImageId { get; set; }
        //Navigation properties
        public ResourceMeta CoverImage { get; set; }
    }
}
