using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderListAPI.Models.Junctions
{
    public class CityDestination
    {
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        [ForeignKey("Destination")]
        public Guid ContentId { get; set; }

        //Navigation properties
        public City City { get; set; }
        public Destination Destination { get; set; }
    }
}