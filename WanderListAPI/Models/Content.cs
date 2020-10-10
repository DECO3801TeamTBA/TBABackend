using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Content
    {
        // Table Properties
        [ForeignKey("Item")]
        public Guid ContentId { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public int Capacity { get; set; }
        public bool Featured { get; set; }
        public int EnvironmentalRating { get; set; }
        public int SocialRating { get; set; }
        public int EconomicRating { get; set; }

        // Navigation Properties i.e FKs to different tables
        public Item Item { get; set; }
    }
}
