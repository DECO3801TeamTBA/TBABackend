using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class City
    {
        // Table Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }

        // Navigation Properties i.e FKs to different tables
        public ResourceMeta CoverImage { get; set; }
    }
}
