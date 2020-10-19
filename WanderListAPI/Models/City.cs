using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace WanderListAPI.Models
{
    public class City
    {
        // Table Properties
        [ForeignKey("Item")]
        public Guid CityId { get; set; }
        public string Country { get; set; }
        public string Video { get; set; }

        // Navigation Properties i.e FKs to different tables
        public Item Item { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
