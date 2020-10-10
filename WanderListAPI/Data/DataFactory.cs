using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MimeKit;
using System;
using System.IO;
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

        public static AppUser CreateUser(string firstName,
            string lastName, string userName)
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

        public static Item CreateItem(string name, string description,
            string fileName, string fileDescription)
        {
            var resourceMeta = CreateResourceMeta(fileName, fileDescription);

            return CreateItem(name, description, resourceMeta);
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

        public static City CreateCity(string name, string description,
            string country,
            string fileName, string fileDescription)
        {
            var resourceMeta = CreateResourceMeta(fileName, fileDescription);
            var item = CreateItem(name, description, resourceMeta);

            return CreateCity(item, country);
        }

        public static Content CreateContent(Item item, int environmentRating,
            int socialRating, int economicRating)
        {
            return new Content()
            {
                ContentId = item.ItemId,
                Item = item,
                EnvironmentalRating = environmentRating,
                SocialRating = socialRating,
                EconomicRating = economicRating
            };
        }

        public static Content CreateContent(string name, string description,
            string fileName, string fileDescription, int environmentRating,
            int socialRating, int economicRating)
        {
            var resourceMeta = CreateResourceMeta(fileName, fileDescription);
            var item = CreateItem(name, description, resourceMeta);

            return CreateContent(item, environmentRating, socialRating,
                economicRating);
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
                ActivityId = content.ContentId,
                Content = content
            };

            return activity;
        }

        public static Activity CreateActivity(string name, string description,
            string fileName, string fileDescription, int environmentRating,
            int socialRating, int economicRating)
        {
            var resourceMeta = CreateResourceMeta(fileName, fileDescription);
            var item = CreateItem(name, description, resourceMeta);
            var content = CreateContent(item, environmentRating, socialRating,
                economicRating);

            return CreateActivity(content);
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
            string description, string fileName, string fileDescription,
            int environmentRating, int socialRating, int economicRating)
        {
            var resourceMeta = CreateResourceMeta(fileName, fileDescription);
            var item = CreateItem(name, description, resourceMeta);
            var content = CreateContent(item, environmentRating, socialRating,
                economicRating);

            return CreateDestination(content);
        }

        public static Shortlist CreateShortlist(string name)
        {
            var shortlist = new Shortlist()
            {
                ShortlistId = Guid.NewGuid(),
                ListName = name
            };

            return shortlist;
        }

        public static Resource CreateResource(string fileName)
        {
            var filePath = "./Resources/Images/" + fileName;
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var memStream = new MemoryStream();
            fileStream.CopyTo(memStream);

            var resource = new Resource()
            {
                ResourceId = Guid.NewGuid(),
                FilePath = filePath,
                Data = memStream.ToArray()
            };

            return resource;
        }

        public static ResourceMeta CreateResourceMeta(Resource resource,
            string description)
        {
            var fileName = Path.GetFileName(resource.FilePath);

            return new ResourceMeta()
            {
                ResourceMetaId = resource.ResourceId,
                AddedOn = DateTime.Now,
                OnDisk = false,
                Description = description,
                FileName = fileName,
                Extension = Path.GetExtension(resource.FilePath),
                // Length?
                MimeType = MimeTypes.GetMimeType(fileName),
                Resource = resource
            };
        }

        public static ResourceMeta CreateResourceMeta(string fileName,
            string description)
        {
            var resource = CreateResource(fileName);

            return CreateResourceMeta(resource, description);
        }

        public static QR CreateQR(Content content)
        {
            return new QR()
            {
                QRId = Guid.NewGuid(),
                Expiry = DateTime.Now.AddMonths(3),
                ContentId = content.ContentId
            };
        }

        // Junctions
        public static CityActivity CreateCityActivity(City city,
            Activity activity)
        {
            return new CityActivity()
            {
                CityId = city.CityId,
                ActivityId = activity.ActivityId
            };
        }

        public static CityDestination CreateCityDestination(City city,
            Destination destination)
        {
            return new CityDestination()
            {
                CityId = city.CityId,
                DestinationId = destination.DestinationId
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

        public static UserShortlist CreateUserShortlist(AppUser user,
            Shortlist shortlist)
        {
            return new UserShortlist()
            {
                UserId = user.Id,
                ShortlistId = shortlist.ShortlistId
            };
        }

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
        }

        public static void Clean(Item item)
        {
            if (item.CoverImage != null)
            {
                Clean(item.CoverImage);
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
    }
}