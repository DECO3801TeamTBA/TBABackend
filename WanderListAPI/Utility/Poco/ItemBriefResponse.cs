using System;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class ItemBriefResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ResourceMeta CoverImage { get; set; }

        public ItemBriefResponse(Activity activity)
        {
            Id = activity.ActivityId;

            var item = activity.Content.Item;

            Name = item.Name;
            CoverImage = item.CoverImage;
        }
        
        public ItemBriefResponse(Content content)
        {
            Id = content.ContentId;

            var item = content.Item;

            Name = item.Name;
            CoverImage = item.CoverImage;
        }
        
        public ItemBriefResponse(Destination destination)
        {
            Id = destination.DestinationId;

            var item = destination.Content.Item;

            Name = item.Name;
            CoverImage = item.CoverImage;
        }
        
        public ItemBriefResponse(Item item)
        {
            Id = item.ItemId;

            Name = item.Name;
            CoverImage = item.CoverImage;
        }
    }
}