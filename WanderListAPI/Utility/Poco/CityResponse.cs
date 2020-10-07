using System;
using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class CityResponse
    {
        public Guid CityId { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ResourceResponse CoverImage { get; set; }

        public CityResponse(City city)
        {
            CityId = city.CityId;
            Country = city.Country;

            var item = city.Item;

            Name = item.Name;
            Description = item.Description;
            CoverImage = new ResourceResponse(item.CoverImage);
        }
    }
}