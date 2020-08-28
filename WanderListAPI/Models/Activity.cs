using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Activity
    {
        [ForeignKey("Content")]
        public Guid ActivityId { get; set; }
        //Activities should be tied to destinations? I think so!
        public Destination Destination { get; set; }

        //activity specific stuff?
    }
}
