﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Destination : Content
    {
        // Table Properties
        [ForeignKey("Content")]
        public Guid DestinationId { get; set; }

        //destination specific stuff?
    }
}
