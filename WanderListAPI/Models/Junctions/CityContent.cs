using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Models.Junctions
{
    public class CityContent
    {
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        [ForeignKey("Content")]
        public Guid ContentId { get; set; }

        //Navigation properties
        public City City { get; set; }
        public Content Content { get; set; }
    }
}
