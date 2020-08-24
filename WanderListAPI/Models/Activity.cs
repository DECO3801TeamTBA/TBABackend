using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models
{
    public class Activity : Content
    {
        //Activities should be tied to destinations? I think so!
        public Destination Destination { get; set; }

        //activity specific stuff?
    }
}
