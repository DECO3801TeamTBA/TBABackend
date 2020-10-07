using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<CityActivity> cityActivities;
        private List<CityDestination> cityDestinations;
        private List<ContentResourceMeta> contentResourceMetas;
        private List<History> histories;
        private List<IdentityUserRole<string>> identityUserRoles;
        private List<ShortlistContent> shortlistContent;
        private List<UserReward> userRewards;
        private List<UserShortlist> userShortlists;

        public DataSeed()
        {
            // Initialise Junction table lists
            cityActivities = new List<CityActivity>();
            cityDestinations = new List<CityDestination>();
            contentResourceMetas = new List<ContentResourceMeta>();
            histories = new List<History>();
            shortlistContent = new List<ShortlistContent>();
            userRewards = new List<UserReward>();
            userShortlists = new List<UserShortlist>();
            identityUserRoles = new List<IdentityUserRole<string>>();

            
            // Data
            cities = GenerateCities();
            identityRoles = GenerateIdentityRoles();
            users = GenerateUsers(identityRoles);
            shortlists = GenerateShortlists(users);
            rewards = GenerateRewards(users);
            activities = GenerateActivities(cities, shortlists, users);
            destinations = GenerateDestinations(cities, shortlists, users);
            qrCodes = GenerateQRCodes(activities, destinations);
        }

        public void AddData(ModelBuilder modelBuilder)
        {
            var items = new List<Item>();
            var content = new List<Content>();
            var resources = new List<Resource>();
            var resourceMetas = new List<ResourceMeta>();

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
            modelBuilder.Entity<CityActivity>().HasData(cityActivities);
            modelBuilder.Entity<CityDestination>().HasData(cityDestinations);
            modelBuilder.Entity<ContentResourceMeta>()
                .HasData(contentResourceMetas);
            modelBuilder.Entity<History>().HasData(histories);
            modelBuilder.Entity<ShortlistContent>().HasData(shortlistContent);
            modelBuilder.Entity<UserReward>().HasData(userRewards);
            modelBuilder.Entity<UserShortlist>().HasData(userShortlists);
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
                {"Velma", DataFactory.CreateUser("Velma", "Dinkley", "Velma")}
            };

            Join(users["Shaggy"], identityRoles["Admin"]);
            Join(users["Scooby"], identityRoles["User"]);
            Join(users["Velma"], identityRoles["User"]);

            return users;
        }

        public Dictionary<string, Shortlist> GenerateShortlists(Dictionary<string, AppUser> users)
        {
            var shortlists = new Dictionary<string, Shortlist>()
            {
                {"Shag Spots", DataFactory.CreateShortlist("Shag Spots") },
                {"Good burger spots", DataFactory.CreateShortlist("Good burger spots") },
                {"Ghost Sightings", DataFactory.CreateShortlist("Ghost Sightings") }
            };

            Join(users["Shaggy"], shortlists["Shag Spots"]);
            Join(users["Scooby"], shortlists["Good burger spots"]);
            Join(users["Velma"], shortlists["Ghost Sightings"]);

            return shortlists;
        }

        public Dictionary<string, City> GenerateCities()
        {
            return new Dictionary<string, City>()
            {
                {"Brisbane", DataFactory.CreateCity("Brisbane", "Captial of QLD",
                    "Australia", "Brisbane.jfif", "Brisbane")},
                {"Sydney", DataFactory.CreateCity("Sydney", "Slow descent into hell",
                    "Australia", "Sydney.jfif", "Sydney")},
                {"Melbourne", DataFactory.CreateCity("Melbourne", "Land of the dead",
                    "Australia", "Melbourne.jfif", "Melbourne")}
            };
        }

        public Dictionary<string, Activity> GenerateActivities(Dictionary<string, City> cities, 
            Dictionary<string, Shortlist> shortlists, Dictionary<string, AppUser> users)
        {
            var activities = new Dictionary<string, Activity>()
            {
                {"Pub Crawl", DataFactory.CreateActivity("Pub Crawl",
                    "Tour Brisbanes best bars and clubs in a night of fun",
                    "PubCrawl.jfif", "PubCrawl", 3, 5, 5)},
                {"Uni tour", DataFactory.CreateActivity("Uni tour",
                    "Visit Brisbanes best universities",
                    "UniTour.jfif", "UniTour", 3, 5, 5)},
                {"Catch Covid", DataFactory.CreateActivity("Catch Covid",
                    "Do the world a favor and remove yourself from this earth",
                    "Covid.png", "Covid", 5, 5, 5)}
            };

            // CityActivities
            Join(cities["Brisbane"], activities["Pub Crawl"]);
            Join(cities["Brisbane"], activities["Uni tour"]);
            Join(cities["Melbourne"], activities["Catch Covid"]);

            // ShortlistContent
            Join(shortlists["Good burger spots"], activities["Pub Crawl"].Content, 1);
            Join(shortlists["Good burger spots"], activities["Uni tour"].Content, 2);
            Join(shortlists["Ghost Sightings"], activities["Catch Covid"].Content, 1);

            // History
            Join(users["Shaggy"], activities["Pub Crawl"].Content);
            Join(users["Scooby"], activities["Pub Crawl"].Content);
            Join(users["Velma"], activities["Uni tour"].Content);

            return activities;
        }

        public Dictionary<string, Destination> GenerateDestinations(Dictionary<string, City> cities,
            Dictionary<string, Shortlist> shortlists, Dictionary<string, AppUser> users)
        {
            var destinations = new Dictionary<string, Destination>()
            {
                {"UQ", DataFactory.CreateDestination("UQ", "The best uni in brisbane",
                    "UQ.jfif", "UQ", 5, 5, 5)},
                {"South Brisbane Cemetery", DataFactory.CreateDestination("South Brisbane Cemetery",
                    "Super spooooky at night",
                    "SouthBrisbaneCemetery.jfif", "SouthBrisbaneCemetery", 4, 3,
                    5)},
                {"Sydney Opera House", DataFactory.CreateDestination("Sydney Opera House",
                    "Australia's most famouse landmark",
                    "OperaHouse.jfif", "OperaHouse", 5, 5, 5)}
            };

            // CityDestinantions
            Join(cities["Brisbane"], destinations["UQ"]);
            Join(cities["Brisbane"], destinations["South Brisbane Cemetery"]);
            Join(cities["Sydney"], destinations["Sydney Opera House"]);

            // ShortlistContent
            Join(shortlists["Good burger spots"], destinations["UQ"].Content, 3);
            Join(shortlists["Ghost Sightings"], destinations["South Brisbane Cemetery"].Content, 2);
            Join(shortlists["Shag Spots"], destinations["Sydney Opera House"].Content, 1);

            // History
            Join(users["Shaggy"], destinations["South Brisbane Cemetery"].Content);
            Join(users["Scooby"], destinations["South Brisbane Cemetery"].Content);
            Join(users["Velma"], destinations["UQ"].Content);

            return destinations;
        }

        public Dictionary<string, Reward> GenerateRewards(Dictionary<string, AppUser> users)
        {
            var rewards = new Dictionary<string, Reward>()
            {
                {"Covid Bonus", DataFactory.CreateReward("Covid Bonus", "Buy 1 get 2 FREE")},
                {"Uni Tour Discount", DataFactory.CreateReward("Uni Tour Discount",
                    "15% Off your next tour")},
                {"Drink Discount", DataFactory.CreateReward("Drink Discount",
                    "$5 OFF a jug of beer with any meal purchase")}
            };

            Join(users["Shaggy"], rewards["Covid Bonus"]);
            Join(users["Velma"], rewards["Uni Tour Discount"]);
            Join(users["Shaggy"], rewards["Drink Discount"]);

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

        public void Join(City city, Activity activity)
        {
            cityActivities.Add(DataFactory.CreateCityActivity(city, activity));
        }

        public void Join(City city, Destination destination)
        {
            cityDestinations.Add(
                DataFactory.CreateCityDestination(city, destination));
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
        }

        public void Join(Shortlist shortlist, Content content, int num)
        {
            shortlistContent.Add(
                DataFactory.CreateShortlistContent(shortlist, content, num));
        }

        public void Join(AppUser user, Shortlist shortlist)
        {
            userShortlists.Add(
                DataFactory.CreateUserShortlist(user, shortlist));
        }

        public void Join(AppUser user, Reward reward)
        {
            userRewards.Add(DataFactory.CreateUserReward(user, reward));
        }
    }
}