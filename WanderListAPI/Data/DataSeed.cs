using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WanderListAPI.Models;
using WanderListAPI.Models.Junctions;

namespace WanderListAPI.Data
{
    public class DataSeed
    {
        private List<AppUser> users;
        private List<City> cities;
        private List<Activity> activities;
        private List<Destination> destinations;
        private List<Reward> rewards;
        private List<Shortlist> shortlists;
        private List<IdentityRole> identityRoles;

        // Junction Tables
        private List<CityActivity> cityActivities;
        private List<CityDestination> cityDestinations;
        private List<ContentResourceMeta> contentResourceMetas;
        private List<History> histories;
        private List<ShortlistContent> shortlistContent;
        private List<UserReward> userRewards;
        private List<UserShortlist> userShortlists;
        private List<IdentityUserRole<string>> identityUserRoles;

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
            identityRoles = GenerateIdentityRoles();
            // Needs to happen after GenerateIdentityRoles()
            users = GenerateUsers();
            cities = GenerateCities();
            shortlists = GenerateShortlists();
            // Needs to Happen After Cities
            activities = GenerateActivities();
            destinations = GenerateDestinations();
            // Needs to happen after users
            rewards = GenerateRewards();
        }

        public void AddData(ModelBuilder modelBuilder)
        {
            foreach (AppUser user in users)
            {
                modelBuilder.Entity<AppUser>().HasData(user);
            }

            var items = new List<Item>();
            var content = new List<Content>();
            var resources = new List<Resource>();
            var resourceMetas = new List<ResourceMeta>();

            foreach (Activity activity in activities)
            {
                content.Add(activity.Content);
                items.Add(activity.Content.Item);
                //resourceMetas.Add(activity.Content.Item.CoverImage);
                //resources.Add(activity.Content.Item.CoverImage.Resource);
                DataFactory.Clean(activity);
            }

            foreach (Destination destination in destinations)
            {
                content.Add(destination.Content);
                items.Add(destination.Content.Item);
                //resourceMetas.Add(destination.Content.Item.CoverImage);
                //resources.Add(destination.Content.Item.CoverImage.Resource);
                DataFactory.Clean(destination);
            }

            foreach (City city in cities)
            {
                items.Add(city.Item);
                //resourceMetas.Add(city.Item.CoverImage);
                //resources.Add(city.Item.CoverImage.Resource);
                DataFactory.Clean(city);
            }

            foreach (AppUser user in users)
            {
                Console.WriteLine(user + "\n");
            }

            modelBuilder.Entity<IdentityRole>().HasData(identityRoles);
            modelBuilder.Entity<AppUser>().HasData(users);
            //modelBuilder.Entity<Item>().HasData(items);
            modelBuilder.Entity<City>().HasData(cities);
            //modelBuilder.Entity<Content>().HasData(content);
            modelBuilder.Entity<Activity>().HasData(activities);
            modelBuilder.Entity<Destination>().HasData(destinations);
            //modelBuilder.Entity<ResourceMeta>().HasData(resourceMetas);
            //modelBuilder.Entity<Resource>().HasData(resources);
            modelBuilder.Entity<Reward>().HasData(rewards);
            modelBuilder.Entity<Shortlist>().HasData(shortlists);

            // Junctions
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

        public List<IdentityRole> GenerateIdentityRoles()
        {
            var identityRoles = new List<IdentityRole>()
            {
                DataFactory.CreateIdentityRole("User"),
                DataFactory.CreateIdentityRole("Admin")
            };

            return identityRoles;
        }

        public List<AppUser> GenerateUsers()
        {
            var users = new List<AppUser>()
            {
                DataFactory.CreateUser("Norville", "Rogers", "Shaggy"),
                DataFactory.CreateUser("Scoobert", "Doo", "Scooby"),
                DataFactory.CreateUser("Velma", "Dinkley", "Velma")
            };

            Join(users[0], identityRoles[1]);
            Join(users[1], identityRoles[0]);
            Join(users[2], identityRoles[0]);

            return users;
        }

        public List<Shortlist> GenerateShortlists()
        {
            var shortlists = new List<Shortlist>()
            {
                DataFactory.CreateShortlist("Shag Spots"),
                DataFactory.CreateShortlist("Good burger spots"),
                DataFactory.CreateShortlist("Ghost Sightings")
            };

            Join(users[0], shortlists[0]);
            Join(users[0], shortlists[1]);
            Join(users[2], shortlists[2]);

            return shortlists;
        }

        public List<City> GenerateCities()
        {
            var cities = new List<City>()
            {
                DataFactory.CreateCity("Brisbane", "Captial of QLD",
                    "Australia", "Brisbane.jfif", "Brisbane"),
                DataFactory.CreateCity("Sydney", "Slow fall into hell",
                    "Australia", "Sydney.jfif", "Sydney"),
                DataFactory.CreateCity("Melbourne", "Land of the dead",
                    "Australia", "Melbourne.jfif", "Melbourne"),
            };

            return cities;
        }

        public List<Activity> GenerateActivities()
        {
            var activities = new List<Activity>()
            {
                DataFactory.CreateActivity("Pub Crawl",
                    "Tour Brisbanes best bars and clubs in a night of fun",
                    "PubCrawl.jfif", "PubCrawl", 3, 5, 5),
                DataFactory.CreateActivity("Uni tour",
                    "Visit Brisbanes best universities",
                    "UniTour.jfif", "UniTour", 3, 5, 5),
                DataFactory.CreateActivity("Catch Covid",
                    "Do the world a favor and remove yourself from this earth",
                    "Covid.png", "Covid", 5, 5, 5)
            };

            // CityActivities
            Join(cities[0], activities[0]);
            Join(cities[0], activities[1]);
            Join(cities[2], activities[2]);

            // ShortlistContent
            Join(shortlists[1], activities[0].Content, 1);
            Join(shortlists[1], activities[1].Content, 2);
            Join(shortlists[2], activities[2].Content, 1);

            // History
            Join(users[0], activities[0].Content);
            Join(users[1], activities[0].Content);
            Join(users[2], activities[1].Content);

            return activities;
        }

        public List<Destination> GenerateDestinations()
        {
            var destinations = new List<Destination>()
            {
                DataFactory.CreateDestination("UQ", "The best uni in brisbane",
                    "UQ.jfif", "UQ", 5, 5, 5),
                DataFactory.CreateDestination("South Brisbane Cemetery",
                    "Super spooooky at night",
                    "SouthBrisbaneCemetery.jfif", "SouthBrisbaneCemetery", 4, 3,
                    5),
                DataFactory.CreateDestination("Sydney Opera House",
                    "Australia's most famouse landmark",
                    "OperaHouse.jfif", "OperaHouse", 5, 5, 5)
            };

            // CityDestinantions
            Join(cities[0], destinations[0]);
            Join(cities[0], destinations[1]);
            Join(cities[1], destinations[2]);

            // ShortlistContent
            Join(shortlists[0], destinations[1].Content, 2);
            Join(shortlists[2], destinations[2].Content, 1);

            // History
            Join(users[2], destinations[0].Content);
            Join(users[0], destinations[1].Content);
            Join(users[1], destinations[1].Content);

            return destinations;
        }

        public List<Reward> GenerateRewards()
        {
            var rewards = new List<Reward>()
            {
                DataFactory.CreateReward("Covid Bonus", "Buy 1 get 2 FREE"),
                DataFactory.CreateReward("Uni Tour Discount",
                    "15% Off your next tour"),
                DataFactory.CreateReward("Drink Discount",
                    "$5 OFF a jug of beer with any meal purchase")
            };

            Join(users[0], rewards[0]);
            Join(users[0], rewards[1]);
            Join(users[2], rewards[2]);

            return rewards;
        }

        public void Join(AppUser user, IdentityRole role)
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