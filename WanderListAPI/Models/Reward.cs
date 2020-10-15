using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Reward
    {
        // Table Properties
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RewardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public int CountThreshold { get; set; }

        //Navigation properties
        public City City { get; set; }
    }
}
