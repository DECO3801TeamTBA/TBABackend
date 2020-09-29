using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderListAPI.Models.Junctions
{
    public class CityActivity
    {
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        [ForeignKey("Activity")]
        public Guid ContentId { get; set; }

        //Navigation properties
        public City City { get; set; }
        public Activity Activity { get; set; }
    }
}
