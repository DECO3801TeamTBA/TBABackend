using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models.Junctions
{
    public class CityUser
    {
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public int Count { get; set; }

        //Navigation properties
        public City City { get; set; }
        public AppUser User { get; set; }
    }
}
