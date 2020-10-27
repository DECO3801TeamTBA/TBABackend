using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class MapResponse
    {
        public int AverageRating { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public ResourceMeta CoverImage { get; set; }
    }
}
