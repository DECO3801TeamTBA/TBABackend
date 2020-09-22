using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;
using WanderListAPI.Models.Junctions;

namespace WanderListAPI.Data
{
    public static class DataFactory
    {
        public static IdentityRole CreateIdentityRole(string type)
        {
            var identityRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = type,
                NormalizedName = type.ToUpper()
            };

            return identityRole;
        }

        public static IdentityUserRole<string> CreateIdentityUserRole(Guid userId, Guid roleId)
        {
            var identityUserRole = new IdentityUserRole<string>()
            {
                RoleId = roleId.ToString(),
                UserId = userId.ToString()
            };

            return identityUserRole;
        }

        public static ApplicationUser CreateApplicationUser()
        {
            var user = new ApplicationUser()
            {
                FirstName = "Norville",
                LastName = "Rogers",
                Id = Guid.NewGuid().ToString(),
                UserName = "Shaggy",
                NormalizedUserName = "SHAGGY",
                NormalizedEmail = "SURFER69@SCOOBYDOO.COM",
                Email = "surfer69@scoobydoo.com",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "1234"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            return user;
        }

        public static Content CreateContent()
        {
            var content = new Content()
            {
                ContentId = Guid.NewGuid()
            };

            return content;
        }

        public static Reward CreateReward()
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

        public static Activity CreateActivity(Content content)
        {
            //content.Name = "Ride in the Mystery.inc truck";
            //content.Description = "Take a guided tour of the towns most " +
            //    "mysterious attractions in a mystery inc truck";
            //content.Capacity = 125;

            var activity = new Activity()
            {
                ActivityId = content.ContentId
            };

            return activity;
        }

        public static Destination CreateDestination(Content content)
        {
            //content.Name = "Scooby Ville";
            //content.Description = "The scooby themed holiday destination";
            //content.Capacity = 50;

            var destination = new Destination()
            {
                DestinationId = content.ContentId
            };

            return destination;
        }

        public static History CreateHistory(string userId, Guid contentId)
        {
            var history = new History()
            {
                UserId = userId.ToString(),
                ContentId = contentId,
                Date = DateTime.Now
            };

            return history;
        }


        public static (Shortlist, UserShortlist) CreateShortlist(string userId)
        {
            var id = Guid.NewGuid();
            var shortlist = new Shortlist()
            {
                ShortlistId = id,
                ListName = "Scooby Doo Vacation"
            };

            var userShortlist = new UserShortlist()
            {
                ShortlistId = id,
                UserId = userId
            };
            return (shortlist, userShortlist);
        }

        public static ShortlistContent CreateShortlistContent(Guid listId, Guid contentId, int number)
        {
            var shortlistContent = new ShortlistContent()
            {
                ShortlistId = listId,
                ContentId = contentId,
                Number = number
            };

            return shortlistContent;
        }

        public static UserReward CreateUserReward(string userId, Guid rewardId)
        {
            var userReward = new UserReward()
            {
                UserId = userId,
                RewardId = rewardId
            };

            return userReward;
        }

        public static (Resource, ResourceMeta) CreateResourceWithMeta()
        {
            string picture1Path = "./Utility/TestImages/photo1.jpg";
            using var fileStream = new FileStream(picture1Path, FileMode.Open);
            using var memStream = new MemoryStream();
            var id = Guid.NewGuid();
            var resource = new Resource();
            var resourceMeta = new ResourceMeta();
            resource.ResourceId = id;
            resourceMeta.ResourceMetaId = id;
            fileStream.CopyTo(memStream); //using sync code after this to demonstrate that it will work either way
            resourceMeta.AddedOn = DateTime.Now;
            resourceMeta.OnDisk = false;
            resourceMeta.Description = "A picture of Brisbane";
            resourceMeta.FileName = Path.GetFileName(picture1Path);
            resourceMeta.Extension = Path.GetExtension(picture1Path);
            resourceMeta.MimeType = MimeTypes.GetMimeType(Path.GetFileName(picture1Path));
            resource.Data = memStream.ToArray();
            return (resource, resourceMeta);
        }

        public static (City, Item) CreateCityWithItem(Guid coverId)
        {
            var id = Guid.NewGuid();
            var item = new Item()
            {
                ItemId = id,
                CoverImageId = coverId,
                Description = "Brissy yeah!",
                Name = "Brisbane"
            };
            var city = new City
            {
                CityId = id,
                Country = "Australia"
            };
            return (city, item);

        }

        public static (Activity, Content, CityContent, Item) CreateActivityWithContentAndItem(Guid coverId, Guid cityId)
        {
            var id = Guid.NewGuid();
            var item = new Item()
            {
                ItemId = id,
                CoverImageId = coverId,
                Description = "You'd have to be nuts!",
                Name = "Swimming the Brisbane River"
            };
            var content = new Content()
            {
                ContentId = id,
                Featured = true,
                Address = "123 The Way Street, Brisbane, 9999",
                EconomicRating = 5,
                SocialRating = 2,
                EnvironmentalRating = 4,
                Longitude = 15.2M,
                Lattitude = 15.2M,
                Capacity = 1500,
                Website = "www.dontdotit.com"
            };
            var cityContent = new CityContent()
            {
                ContentId = id,
                CityId = cityId
            };
            var activity = new Activity()
            {
                ActivityId = id
            };

            return (activity, content, cityContent, item);
        }
        public static (Destination, Content, CityContent, Item) CreateDestinationWithContentAndItem(Guid coverId, Guid cityId)
        {
            var id = Guid.NewGuid();
            var item = new Item()
            {
                ItemId = id,
                CoverImageId = coverId,
                Description = "All are welcome",
                Name = "Bob's House"
            };
            var content = new Content()
            {
                ContentId = id,
                Featured = true,
                Address = "125 The Way Street, Brisbane, 9997",
                EconomicRating = 5,
                SocialRating = 5,
                EnvironmentalRating = 3,
                Longitude = 15.4M,
                Lattitude = 15.2M,
                Capacity = 12,
                Website = "www.bobshouse.com"
            };
            var cityContent = new CityContent()
            {
                ContentId = id,
                CityId = cityId
            };
            var destination = new Destination()
            {
                DestinationId = id
            };

            return (destination, content, cityContent, item);
        }

        public static ContentResourceMeta CreateContentResourceMeta(Guid resourceMetaId,
            Guid contentId, int number)
        {
            var contentResourceMeta = new ContentResourceMeta()
            {
                Number = number,
                ContentId = contentId,
                ResourceMetaId = resourceMetaId
            };
            return contentResourceMeta;
        }
    }
}
