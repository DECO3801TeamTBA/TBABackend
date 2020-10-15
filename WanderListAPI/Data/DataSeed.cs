using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WanderListAPI.Models;
using WanderListAPI.Models.Junctions;

namespace WanderListAPI.Data
{
    public class DataSeed
    {
        private Dictionary<string, Activity> activities;
        private Dictionary<string, City> cities;
        private Dictionary<string, Destination> destinations;
        private Dictionary<string, IdentityRole> identityRoles;
        private List<QR> qrCodes;
        private Dictionary<string, Reward> rewards;
        private Dictionary<string, Shortlist> shortlists;
        private Dictionary<string, AppUser> users;

        // Junction Tables
        private List<CityUser> cityUsers;
        private List<ContentResourceMeta> contentResourceMetas;
        private List<History> histories;
        private List<IdentityUserRole<string>> identityUserRoles;
        private List<ShortlistContent> shortlistContent;
        private List<UserReward> userRewards;

        public DataSeed()
        {
            // Initialise Junction table lists
            cityUsers = new List<CityUser>();
            contentResourceMetas = new List<ContentResourceMeta>();
            histories = new List<History>();
            shortlistContent = new List<ShortlistContent>();
            userRewards = new List<UserReward>();
            identityUserRoles = new List<IdentityUserRole<string>>();

            
            // Data
            cities = GenerateCities();
            rewards = GenerateRewards(cities);
            identityRoles = GenerateIdentityRoles();
            users = GenerateUsers(identityRoles);
            shortlists = GenerateShortlists(users);
            activities = GenerateActivities(cities, shortlists, users);
            destinations = GenerateDestinations(cities, shortlists, users);
            qrCodes = GenerateQRCodes(activities, destinations);

            GenerateUserRewards(users, rewards, cityUsers);
        }

        public void AddData(ModelBuilder modelBuilder)
        {
            var items = new List<Item>();
            var content = new List<Content>();
            var resources = new List<Resource>();
            var resourceMetas = new List<ResourceMeta>();

            foreach (AppUser user in users.Values)
            {
                resourceMetas.Add(user.ProfilePic);
                resources.Add(user.ProfilePic.Resource);
                DataFactory.Clean(user);
            }

            foreach (Activity activity in activities.Values)
            {
                content.Add(activity.Content);
                items.Add(activity.Content.Item);
                resourceMetas.Add(activity.Content.Item.CoverImage);
                resources.Add(activity.Content.Item.CoverImage.Resource);
                DataFactory.Clean(activity);
            }

            foreach (Destination destination in destinations.Values)
            {
                content.Add(destination.Content);
                items.Add(destination.Content.Item);
                resourceMetas.Add(destination.Content.Item.CoverImage);
                resources.Add(destination.Content.Item.CoverImage.Resource);
                DataFactory.Clean(destination);
            }

            foreach (City city in cities.Values)
            {
                items.Add(city.Item);
                resourceMetas.Add(city.Item.CoverImage);
                resources.Add(city.Item.CoverImage.Resource);
                DataFactory.Clean(city);
            }

            // Add Dictionaries
            modelBuilder.Entity<IdentityRole>().HasData(identityRoles.Values);
            modelBuilder.Entity<AppUser>().HasData(users.Values);
            modelBuilder.Entity<City>().HasData(cities.Values);
            modelBuilder.Entity<Activity>().HasData(activities.Values);
            modelBuilder.Entity<Destination>().HasData(destinations.Values);
            modelBuilder.Entity<Reward>().HasData(rewards.Values);
            modelBuilder.Entity<Shortlist>().HasData(shortlists.Values);

            // Add Lists
            modelBuilder.Entity<QR>().HasData(qrCodes);
            modelBuilder.Entity<Item>().HasData(items);
            modelBuilder.Entity<Content>().HasData(content);
            modelBuilder.Entity<ResourceMeta>().HasData(resourceMetas);
            modelBuilder.Entity<Resource>().HasData(resources);

            // Add Junctions
            modelBuilder.Entity<CityUser>().HasData(cityUsers);
            modelBuilder.Entity<ContentResourceMeta>()
                .HasData(contentResourceMetas);
            modelBuilder.Entity<History>().HasData(histories);
            modelBuilder.Entity<ShortlistContent>().HasData(shortlistContent);
            modelBuilder.Entity<UserReward>().HasData(userRewards);
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData((identityUserRoles));
        }

        public Dictionary<string, IdentityRole> GenerateIdentityRoles()
        {
            return new Dictionary<string, IdentityRole>()
            {
                {"User", DataFactory.CreateIdentityRole("User")},
                {"Admin", DataFactory.CreateIdentityRole("Admin")}
            };
        }

        public Dictionary<string, AppUser> GenerateUsers(Dictionary<string, IdentityRole> identityRoles)
        {
            var users = new Dictionary<string, AppUser>()
            {
                {"Shaggy", DataFactory.CreateUser("Norville", "Rogers", "Shaggy")},
                {"Scooby", DataFactory.CreateUser("Scoobert", "Doo", "Scooby")},
            };

            Join(users["Shaggy"], identityRoles["Admin"]);
            Join(users["Scooby"], identityRoles["User"]);

            return users;
        }

        public Dictionary<string, Shortlist> GenerateShortlists(Dictionary<string, AppUser> users)
        {
            var shortlists = new Dictionary<string, Shortlist>()
            {
                {"Shag Spots", DataFactory.CreateShortlist("Shag Spots", users["Shaggy"]) },
                {"Good burger spots", DataFactory.CreateShortlist("Good burger spots", users["Shaggy"]) },
                {"Ghost Sightings", DataFactory.CreateShortlist("Ghost Sightings", users["Shaggy"]) }
            };

            return shortlists;
        }

        public Dictionary<string, City> GenerateCities()
        {
            return new Dictionary<string, City>()
            {
                {"Brisbane", DataFactory.CreateCity("Brisbane", "Captial of QLD",
                    "Australia", "nDHlEG48b-M", -27.46794, 153.02809, "Brisbane.jfif", "Brisbane")},
                {"Sydney", DataFactory.CreateCity("Sydney", "Slow descent into hell",
                    "Australia", "Yc7r_bbt00M", -33.86785, 151.20732, "Sydney.jfif", "Sydney")},
                {"Melbourne", DataFactory.CreateCity("Melbourne", "Land of the dead",
                    "Australia", "Rzn5WGnS350", -37.814, 144.96332, "Melbourne.jfif", "Melbourne")}
            };
        }

        public Dictionary<string, Activity> GenerateActivities(Dictionary<string, City> cities, 
            Dictionary<string, Shortlist> shortlists, Dictionary<string, AppUser> users)
        {
            var activities = new Dictionary<string, Activity>()
            {
                {"Pub Crawl", DataFactory.CreateActivity("Pub Crawl",
                    "Tour Brisbanes best bars and clubs in a night of fun",
                    "PubCrawl.jfif", "PubCrawl", 3, 5, 5, -27.470568, 153.024866, cities["Brisbane"])},
                {"Uni tour", DataFactory.CreateActivity("Uni tour",
                    "Visit Brisbanes best universities",
                    "UniTour.jfif", "UniTour", 3, 5, 5, -27.477119, 153.028372, cities["Brisbane"])},
                {"Catch Covid", DataFactory.CreateActivity("Catch Covid",
                    "Do the world a favor and remove yourself from this earth",
                    "Covid.png", "Covid", 5, 5, 5, -33.86785, 151.20732, cities["Melbourne"])}
            };

            // ShortlistContent
            Join(shortlists["Good burger spots"], activities["Pub Crawl"].Content, 1);
            Join(shortlists["Good burger spots"], activities["Uni tour"].Content, 2);
            Join(shortlists["Ghost Sightings"], activities["Catch Covid"].Content, 1);

            // History
            Join(users["Shaggy"], activities["Pub Crawl"].Content);
            Join(users["Scooby"], activities["Pub Crawl"].Content);
            Join(users["Scooby"], activities["Uni tour"].Content);

            return activities;
        }

        public Dictionary<string, Destination> GenerateDestinations(Dictionary<string, City> cities,
            Dictionary<string, Shortlist> shortlists, Dictionary<string, AppUser> users)
        {
            var destinations = new Dictionary<string, Destination>()
            {
                {"UQ", DataFactory.CreateDestination("UQ", "The best uni in brisbane",
                    "UQ.jfif", "UQ", 5, 5, 5,-27.497408, 153.013680, cities["Brisbane"])},
                {"South Brisbane Cemetery", DataFactory.CreateDestination("South Brisbane Cemetery",
                    "Super spooooky at night",
                    "SouthBrisbaneCemetery.jfif", "SouthBrisbaneCemetery", 4, 3,
                    5, -27.498973, 153.027120, cities["Brisbane"])},
                {"Sydney Opera House", DataFactory.CreateDestination("Sydney Opera House",
                    "Australia's most famouse landmark",
                    "OperaHouse.jfif", "OperaHouse", 5, 5, 5, -33.856651, 151.215276, cities["Sydney"])}
            };

            // ShortlistContent
            Join(shortlists["Good burger spots"], destinations["UQ"].Content, 3);
            Join(shortlists["Ghost Sightings"], destinations["South Brisbane Cemetery"].Content, 2);
            Join(shortlists["Shag Spots"], destinations["Sydney Opera House"].Content, 1);

            // History
            Join(users["Shaggy"], destinations["South Brisbane Cemetery"].Content);
            Join(users["Scooby"], destinations["South Brisbane Cemetery"].Content);

            return destinations;
        }

        public Dictionary<string, Reward> GenerateRewards(Dictionary<string, City> cities)
        {
            var rewards = new Dictionary<string, Reward>()
            {
                {"Covid Bonus", DataFactory.CreateReward("Covid Bonus", "Buy 1 get 2 FREE", cities["Mebourne"], 0)},
                {"Uni Tour Discount", DataFactory.CreateReward("Uni Tour Discount",
                    "15% Off your next tour", cities["Brisbane"], 1)},
                {"Drink Discount", DataFactory.CreateReward("Drink Discount",
                    "$5 OFF a jug of beer with any meal purchase", cities["Brisbane"], 3)}
            };

            return rewards;
        }

        public List<QR> GenerateQRCodes(Dictionary<string, Activity> activities, Dictionary<string, Destination> destinations)
        {
            var qrCodes = new List<QR>();
            foreach (Activity activity in activities.Values) {
                qrCodes.Add(DataFactory.CreateQR(activity.Content));
            }

            foreach (Destination destination in destinations.Values)
            {
                qrCodes.Add(DataFactory.CreateQR(destination.Content));
            }

            return qrCodes;
        }

        public void Join(AppUser user, IdentityRole<string> role)
        {
            identityUserRoles.Add(
                DataFactory.CreateIdentityUserRole(user, role));
        }

        public void Join(Content content, ResourceMeta resourceMeta, int num)
        {
            contentResourceMetas.Add(
                DataFactory.CreateContentResourceMeta(content, resourceMeta,
                    num));
        }

        public void Join(AppUser user, Content content)
        {
            histories.Add(DataFactory.CreateHistory(user, content));
            var entry = cityUsers.Find(cuse => cuse.UserId == user.Id && cuse.CityId == content.CityId);
            if (entry == default(CityUser))
            {
                cityUsers.Add(DataFactory.CreateCityUser(content.City, user, 1));
            } else
            {
                entry.Count++;
            }
        }

        public void Join(Shortlist shortlist, Content content, int num)
        {
            shortlistContent.Add(
                DataFactory.CreateShortlistContent(shortlist, content, num));
        }

        public void Join(AppUser user, Reward reward)
        {
            userRewards.Add(DataFactory.CreateUserReward(user, reward));
        }

        public void GenerateUserRewards(Dictionary<string, AppUser> users,
            Dictionary<string, Reward> rewards, List<CityUser> cityUsers)
        {
            foreach (CityUser cuse in cityUsers)
            {
                var user = users.Values.ToList().Find(use => use.Id == cuse.UserId);
                var rewList = rewards.Values.ToList().FindAll(rew => rew.CityId == cuse.CityId);
                foreach (Reward reward in rewList)
                {
                    Join(user, reward);
                }
            }
        }
    }
}