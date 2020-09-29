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

        public static IdentityUserRole<string> CreateIdentityUserRole(
            AppUser user, IdentityRole role)
        {
            var identityUserRole = new IdentityUserRole<string>()
            {
                RoleId = role.Id.ToString(),
                UserId = user.Id
            };

            return identityUserRole;
        }

        public static AppUser CreateAppUser(string firstName,
            string lastName, string UserName)
        {
            string email = firstName + '.' + lastName + "@pretend.com";

            var user = new AppUser()
            {
                FirstName = firstName,
                LastName = lastName,
                Id = Guid.NewGuid().ToString(),
                UserName = firstName,
                NormalizedUserName = firstName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),

                //Need this?
                PasswordHash =
                    new PasswordHasher<AppUser>().HashPassword(null, "1234"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            return user;
        }

        public static Item CreateItem(string name, string description,
            ResourceMeta coverImage)
        {
            var item = new Item()
            {
                ItemId = Guid.NewGuid(),
                Name = name,
                Description = description,
                CoverImageId = coverImage.ResourceMetaId,
                CoverImage = coverImage
            };

            return item;
        }

        public static City CreateCity(Item item, string country)
        {
            var city = new City()
            {
                CityId = item.ItemId,
                Country = country,
                Item = item
            };

            return city;
        }

        public static Content CreateContent(Item item, int environmentRating, int socialRating, int economicRating)
        {
            var content = new Content()
            {
                ContentId = item.ItemId,
                EnvironmentalRating = environmentRating,
                SocialRating = socialRating,
                EconomicRating = economicRating
            };

            return content;
        }

        public static Reward CreateReward(string name, string value)
        {
            var reward = new Reward()
            {
                RewardId = Guid.NewGuid(),
                Name = name,
                Value = value,
                ExpiryDate = new DateTime()
            };

            return reward;
        }

        public static Activity CreateActivity(Content content)
        {
            var activity = new Activity()
            {
                ActivityId = content.ContentId
            };

            return activity;
        }

        public static Destination CreateDestination(Content content)
        {
            var destination = new Destination()
            {
                DestinationId = content.ContentId
            };

            return destination;
        }

        public static History CreateHistory(AppUser user, Content content)
        {
            var history = new History()
            {
                UserId = user.Id,
                ContentId = content.ContentId,
                Date = DateTime.Now
            };

            return history;
        }


        //public static (Shortlist, UserShortlist) CreateShortlist(string name)
        public static Shortlist CreateShortlist(string name)
        {
            var shortlist = new Shortlist()
            {
                ShortlistId = Guid.NewGuid(),
                ListName = name
            };

            return shortlist;
        }

        public static ShortlistContent CreateShortlistContent(Shortlist list, Content content, int num)
        {
            var shortlistContent = new ShortlistContent()
            {
                ShortlistId = list.ShortlistId,
                ContentId = content.ContentId,
                Number = num
            };

            return shortlistContent;
        }

        public static UserReward CreateUserReward(AppUser user, Reward reward)
        {
            var userReward = new UserReward()
            {
                UserId = user.Id,
                RewardId = reward.RewardId
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
            fileStream
                .CopyTo(
                    memStream); //using sync code after this to demonstrate that it will work either way
            resourceMeta.AddedOn = DateTime.Now;
            resourceMeta.OnDisk = false;
            resourceMeta.Description = "A picture of Brisbane";
            resourceMeta.FileName = Path.GetFileName(picture1Path);
            resourceMeta.Extension = Path.GetExtension(picture1Path);
            resourceMeta.MimeType =
                MimeTypes.GetMimeType(Path.GetFileName(picture1Path));
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

        public static (Activity, Content, CityActivity, Item)
            CreateActivityWithContentAndItem(Guid coverId,
                Guid cityId)
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
            var cityContent = new CityActivity()
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

        public static (Destination, Content, CityActivity, Item)
            CreateDestinationWithContentAndItem(Guid coverId,
                Guid cityId)
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
            var cityContent = new CityActivity()
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

        public static ContentResourceMeta CreateContentResourceMeta(
            Guid resourceMetaId,
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