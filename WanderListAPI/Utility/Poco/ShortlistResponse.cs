using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Utility.Poco
{
    public class ShortlistResponse
    {
        public Guid ShortlistId { get; set; }
        public string ListName { get; set; }
        public ResourceResponse CoverImage { get; set; }
    }
}
