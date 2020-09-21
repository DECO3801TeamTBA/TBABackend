using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class UserReward
    {
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("Reward")]
        public Guid RewardId { get; set; }

        //Navigation properties
        public Reward Reward { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
