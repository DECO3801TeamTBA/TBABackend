using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Destination
    {
        [ForeignKey("Content")]
        public Guid DestinationId { get; set; }
        // we don't know what belongs here yet

    }
}
