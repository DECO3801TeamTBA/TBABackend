using System;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class ActivityResponse
    {
        public Guid ActivityId { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public int Capacity { get; set; }
        public bool Featured { get; set; }
        public int EnvironmentalRating { get; set; }
        public int SocialRating { get; set; }
        public int EconomicRating { get; set; }
        public int AverageRating { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ResourceResponse CoverImage { get; set; }

        public ActivityResponse(Activity activity)
        {
            ActivityId = activity.ActivityId;

            var content = activity.Content;

            Lattitude = content.Item.Lattitude;
            Longitude = content.Item.Longitude;
            Address = content.Address;
            Website = content.Website;
            Capacity = content.Capacity;
            Featured = content.Featured;
            EnvironmentalRating = content.EnvironmentalRating;
            SocialRating = content.SocialRating;
            EconomicRating = content.EconomicRating;
            AverageRating = (EnvironmentalRating + SocialRating + EconomicRating) / 3;

            var item = content.Item;

            Name = item.Name;
            Description = item.Description;
            CoverImage = new ResourceResponse(item.CoverImage);
        }
    }
}