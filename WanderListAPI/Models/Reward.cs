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
        public String Name { get; set; }
        public String Description { get; set; }
        public String Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
