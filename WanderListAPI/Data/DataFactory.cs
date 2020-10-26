using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading.Tasks.Dataflow;
using WanderListAPI.Models;
using WanderListAPI.Models.Junctions;

namespace WanderListAPI.Data
{
    public static class DataFactory
    {
        public static Activity CreateActivity(Content content)
        {
            var activity = new Activity()
            {
                ActivityId = content.ContentId,
                Content = content
            };

            return activity;
        }

        public static Activity CreateActivity(string name, string description,
            string fileName, int environmentRating, int socialRating, 
            int economicRating, double latitude, double longitude, City city)
        {
            var resourceMeta = CreateResourceMeta(fileName);
            var item = CreateItem(name, description, resourceMeta, latitude, longitude);
            var content = CreateContent(item, environmentRating, socialRating,
                economicRating, city);

            return CreateActivity(content);
        }

        public static City CreateCity(Item item, string country, string video)
        {
            var city = new City()
            {
                CityId = item.ItemId,
                Country = country,
                Item = item,
                Video = video
            };

            return city;
        }

        public static City CreateCity(string name, string description,
            string country, string video, double latitude, double longitude,
            string fileName)
        {
            var resourceMeta = CreateResourceMeta(fileName);
            var item = CreateItem(name, description, resourceMeta, latitude, longitude);

            return CreateCity(item, country, video);
        }

        public static Content CreateContent(Item item, int environmentRating,
            int socialRating, int economicRating, City city)
        {
            return new Content()
            {
                ContentId = item.ItemId,
                Item = item,
                EnvironmentalRating = environmentRating,
                SocialRating = socialRating,
                EconomicRating = economicRating,
                CityId = city.CityId,
                City = city,
                Capacity = 5
            };
        }

        public static Content CreateContent(string name, string description,
            string fileName, int environmentRating, int socialRating, int economicRating, double latitude, double longitude, City city)
        {
            var resourceMeta = CreateResourceMeta(fileName);
            var item = CreateItem(name, description, resourceMeta, latitude, longitude);

            return CreateContent(item, environmentRating, socialRating,
                economicRating, city);
        }

        public static Destination CreateDestination(Content content)
        {
            var destination = new Destination()
            {
                DestinationId = content.ContentId,
                Content = content
            };

            return destination;
        }

        public static Destination CreateDestination(string name,
            string description, string fileName, int environmentRating, 
            int socialRating, int economicRating, double latitude, double longitude, City city)
        {
            var resourceMeta = CreateResourceMeta(fileName);
            var item = CreateItem(name, description, resourceMeta, latitude, longitude);
            var content = CreateContent(item, environmentRating, socialRating,
                economicRating, city);

            return CreateDestination(content);
        }

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

        public static List<ResourceMeta> CreateImageGallery(Item item)
        {
            String name = item.CoverImage.Description;
            return new List<ResourceMeta>()
            {
                item.CoverImage,
                CreateResourceMeta(name + " - 2.jpg"),
                CreateResourceMeta(name + " - 3.jpg")
            };
        }

        public static Item CreateItem(string name, string description,
            ResourceMeta coverImage, double latitude, double longitude)
        {
            var item = new Item()
            {
                ItemId = Guid.NewGuid(),
                Name = name,
                Description = description,
                CoverImageId = coverImage.ResourceMetaId,
                CoverImage = coverImage,
                Lattitude = latitude,
                Longitude = longitude
            };

            return item;
        }

        public static Item CreateItem(string name, string description, double latitude, double longitude,
            string fileName)
        {
            var resourceMeta = CreateResourceMeta(fileName);

            return CreateItem(name, description, resourceMeta, latitude, longitude);
        }

        public static QR CreateQR(Content content, Guid id)
        {
            return new QR()
            {
                QRId = id,
                Expiry = DateTime.Now.AddMonths(3),
                ContentId = content.ContentId
            };
        }

        public static Resource CreateResource(string fileName)
        {
            var filePath = "./Resources/Images/" + fileName;
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var memStream = new MemoryStream();
            //fileStream.CopyTo(memStream);

            var resource = new Resource()
            {
                ResourceId = Guid.NewGuid(),
                FilePath = filePath
            };

            return resource;
        }

        public static ResourceMeta CreateResourceMeta(Resource resource)
        {
            var fileName = Path.GetFileName(resource.FilePath);

            return new ResourceMeta()
            {
                ResourceMetaId = resource.ResourceId,
                AddedOn = DateTime.Now,
                OnDisk = true,
                Description = Path.GetFileNameWithoutExtension(resource.FilePath),
                FileName = fileName,
                Extension = Path.GetExtension(resource.FilePath),
                // Length?
                MimeType = MimeTypes.GetMimeType(fileName),
                Resource = resource
            };
        }

        public static ResourceMeta CreateResourceMeta(string fileName)
        {
            var resource = CreateResource(fileName);

            return CreateResourceMeta(resource);
        }

        public static Reward CreateReward(string name, string value, City city, int threshold, ResourceMeta coverImage)
        {
            return new Reward()
            {
                RewardId = Guid.NewGuid(),
                Name = name,
                Value = value,
                ExpiryDate = new DateTime(),
                CityId = city.CityId,
                CountThreshold = threshold,
                CoverImageId = coverImage.ResourceMetaId,
                CoverImage = coverImage
            };
        }

        public static Reward CreateReward(string name, string value, City city, int threshold, String filename)
        {
            var coverImage = CreateResourceMeta(filename);

            return CreateReward(name, value, city, threshold, coverImage);
        }

        public static Shortlist CreateShortlist(string name, AppUser user)
        {
            var shortlist = new Shortlist()
            {
                ShortlistId = Guid.NewGuid(),
                ListName = name,
                UserId = user.Id
            };

            return shortlist;
        }

        public static AppUser CreateUser(string firstName,
            string lastName, string userName, int points)
        {
            string email = firstName + '.' + lastName + "@pretend.com";
            ResourceMeta profile= CreateResourceMeta("DefaultUser.jfif");

            var user = new AppUser()
            {
                FirstName = firstName,
                LastName = lastName,
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                ProfilePicResourceMetaId = profile.ResourceMetaId,
                ProfilePic = profile,
                Points = points,

                //Need this?
                PasswordHash =
                    new PasswordHasher<AppUser>().HashPassword(null, "1234"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            return user;
        }

        // Junctions
        public static CityUser CreateCityUser(City city, AppUser user, int num)
        {
            return new CityUser()
            {
                CityId = city.CityId,
                UserId = user.Id,
                Count = num
            };
        }

        public static ContentResourceMeta CreateContentResourceMeta(
            Content content, ResourceMeta resourceMeta, int num)
        {
            return new ContentResourceMeta()
            {
                ContentId = content.ContentId,
                ResourceMetaId = resourceMeta.ResourceMetaId,
                Number = num
            };
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

        public static IdentityUserRole<string> CreateIdentityUserRole(
            AppUser user, IdentityRole<string> role)
        {
            var identityUserRole = new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            return identityUserRole;
        }

        public static ShortlistContent CreateShortlistContent(
            Shortlist shortlist, Content content, int num)
        {
            return new ShortlistContent()
            {
                ShortlistId = shortlist.ShortlistId,
                ContentId = content.ContentId,
                Number = num
            };
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

        // Cleaners
        public static void Clean(Activity activity)
        {
            if (activity.Content != null)
            {
                Clean(activity.Content);
                activity.Content = null;
            }
        }

        public static void Clean(Destination destination)
        {
            if (destination.Content != null)
            {
                Clean(destination.Content);
                destination.Content = null;
            }
        }

        public static void Clean(City city)
        {
            if (city.Item != null)
            {
                Clean(city.Item);
                city.Item = null;
            }
        }

        public static void Clean(Content content)
        {
            if (content.Item != null)
            {
                Clean(content.Item);
                content.Item = null;
            }

            if (content.City != null)
            {
                content.City = null;
            }
        }

        public static void Clean(Item item)
        {
            if (item.CoverImage != null)
            {
                item.CoverImage = null;
            }
        }

        public static void Clean(ResourceMeta resourceMeta)
        {
            if (resourceMeta.Resource != null)
            {
                resourceMeta.Resource = null;
            }
        }

        public static void Clean(AppUser user)
        {
            if (user.ProfilePic != null)
            {
                user.ProfilePic = null;
            }
        }

        public static void Clean(Reward reward)
        {
            if (reward.CoverImage != null)
            {
                reward.CoverImage = null;
            }
        }
    }
}