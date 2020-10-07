using WanderListAPI.Models;

namespace WanderListAPI.Utility.Poco
{
    public class UserResponseBrief
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Points { get; set; }

        public UserResponseBrief(AppUser user)
        {
            UserId = user.Id;
            UserName = user.UserName;
            Points = user.Points;
        }
    }
}