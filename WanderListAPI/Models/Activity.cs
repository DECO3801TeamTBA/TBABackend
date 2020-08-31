using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Activity
    {
        // Table Properties
        [ForeignKey("Content")]
        public Guid ActivityId { get; set; }

        //activity specific stuff?

        //Navigation property
        public Content Content { get; set; }
    }
}
