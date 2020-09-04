using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;

namespace WanderListAPI.Data
{
    public class DataFactory
    {
        public IdentityRole CreateIdentityRole(string type)
        {
            var identityRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = type,
                NormalizedName = type
            };

            return identityRole;
        }

        public IdentityUserRole<string> CreateIdentityUserRole(Guid userId, Guid roleId)
        {
            var identityUserRole = new IdentityUserRole<string>()
            {
                RoleId = roleId.ToString(),
                UserId = userId.ToString()
            };

            return identityUserRole;
        }

        public ApplicationUser CreateApplicationUser()
        {
            var user = new ApplicationUser()
            {
                FirstName = "Norville",
                LastName = "Rogers",
                Id = Guid.NewGuid().ToString(),
                UserName = "Shaggy",
                Email = "surfer69@scoobydoo.com",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "1234"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            return user;
        }

        public Content CreateContent()
        {
            var content = new Content()
            {
                ContentId = Guid.NewGuid()
            };

            return content;
        }

        public Reward CreateReward()
        {
            var reward = new Reward()
            {
                RewardId = Guid.NewGuid(),
                Name = "Burger King Coupon",
                Value = "15% OFF",
                ExpiryDate = new DateTime()
            };

            return reward;
        }

        public Activity CreateActivity(Content content)
        {
            content.Name = "Ride in the Mystery.inc truck";
            content.Description = "Take a guided tour of the towns most " +
                "mysterious attractions in a mystery inc truck";
            content.Capacity = 125;

            var activity = new Activity()
            {
                ActivityId = content.ContentId
            };

            return activity;
        }

        public Destination CreateDestination(Content content)
        {
            content.Name = "Scooby Ville";
            content.Description = "The scooby themed holiday destination";
            content.Capacity = 50;

            var destination = new Destination()
            {
                DestinationId = content.ContentId
            };

            return destination;
        }

        public History CreateHistory(Guid userId, Guid contentId)
        {
            var history = new History()
            {
                UserId = userId.ToString(),
                ContentId = contentId,
                Date = DateTime.Now
            };

            return history;
        }


        public Shortlist CreateShortlist(Guid userId)
        {
            var shortlist = new Shortlist()
            {
                UserId = userId.ToString(),
                ShortlistId = Guid.NewGuid(),
                ListName = "Scooby Doo Vacation"
            };

            return shortlist;
        }

        public ShortlistContent CreateShortlistContent(Guid listId, Guid contentId, int number)
        {
            var shortlistContent = new ShortlistContent()
            {
                ShortlistId = listId,
                ContentId = contentId,
                Number = number
            };

            return shortlistContent;
        }

        public UserReward CreateUserReward(Guid userId, Guid rewardId)
        {
            var userReward = new UserReward()
            {
                UserId = userId,
                RewardId = rewardId
            };

            return userReward;
        }
    }
}
