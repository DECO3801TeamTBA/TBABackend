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
        [ForeignKey("City")]
        public Guid CityId { get; set; }

        //Navigation properties
        public Item Item { get; set; }
        public City City { get; set; }
    }
}
