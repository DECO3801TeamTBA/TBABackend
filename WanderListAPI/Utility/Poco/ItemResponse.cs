using System;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class ItemResponse
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ResourceResponse CoverImage { get; set; }

        public ItemResponse(Item item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
            Description = item.Description;
            CoverImage = new ResourceResponse(item.CoverImage);
        }
    }
}
