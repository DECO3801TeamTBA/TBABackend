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
        public Guid UserId { get; set; }
        [ForeignKey("Reward")]
        public Guid RewardId { get; set; }
    }
}
