using System.Collections.Generic;

namespace WanderListAPI.Utility.Poco
{
    public class CityContentResponse
    {
        public List<ItemBriefResponse> Activities { get; set; }
        public List<ItemBriefResponse> Destinations { get; set; }
        public CityContentResponse(List<ItemBriefResponse> activities, List<ItemBriefResponse> destinations)
        {
            Activities = activities;
            Destinations = destinations;
        }
    }
}
