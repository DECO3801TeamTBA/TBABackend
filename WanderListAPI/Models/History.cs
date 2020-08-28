using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WanderListAPI.Models
{
    public class History
    {   
        [ForeignKey("Content"), Key]
        public Guid ContentId { get; set; }
        [ForeignKey("WanderUser"), Key]
        public Guid WanderUserId { get; set; }
        [Timestamp, Key]
        public DateTime Date { get; set; }

        //Navigation Properties
        public Content Content { get; set; }
        public WanderUser WanderUser { get; set; }
    }
}
