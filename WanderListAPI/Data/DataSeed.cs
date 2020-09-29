using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class DataSeed
    {
        private List<AppUser> Users;
        private List<City> Cities;
        private List<Activity> Activities;
        private List<Destination> Destinations;
        private List<Reward> Rewards;
        private List<Shortlist> Shortlists;
        private List<IdentityRole> IdentityRoles;

        // Junction Tables
        private List<CityActivity> CityActivities;
        private List<CityDestination> CityDestinations;
        private List<ContentResourceMeta> ContentResourceMetas;
        private List<History> Histories;
        private List<ShortlistContent> ShortlistContent;
        private List<UserReward> UserRewards;
        private List<UserShortlist> UserShortlists;
        private List<IdentityUserRole<string>> IdentityUserRoles;

        public DataSeed()
        {
            // Initialise Junction table lists
            CityActivities = new List<CityActivity>();
            CityDestinations = new List<CityDestination>();
            ContentResourceMetas = new List<ContentResourceMeta>();
            Histories = new List<History>();
            ShortlistContent = new List<ShortlistContent>();
            UserRewards = new List<UserReward>();
            UserShortlists = new List<UserShortlist>();

            // Data
            IdentityRoles = GenerateIdentityRoles();
            // Needs to happen after GenerateIdentityRoles()
            Users = GenerateUsers();
            Cities = GenerateCities();
            Shortlists = GenerateShortlists();
            // Needs to Happen After Cities
            Activities = GenerateActivities();
            Destinations = GenerateDestinations();
            // Needs to happen after users
            Rewards = GenerateRewards();
        }

        public void addData(ModelBuilder modelBuilder)
        {
            foreach (AppUser user in Users)
            {
                modelBuilder.Entity<AppUser>().HasData(user);
            }

            var Items = new List<Item>();
            var Content = new List<Content>();
            var Resources = new List<Resource>();
            var ResourceMetas = new List<ResourceMeta>();

            foreach (Activity activity in Activities)
            {
                Content.Add(activity.Content);
                Items.Add(activity.Content.Item);
                ResourceMetas.Add(activity.Content.Item.CoverImage);
                Resources.Add(activity.Content.Item.CoverImage.Resource);
            }

            foreach (Destination destination in Destinations)
            {
                Content.Add(destination.Content);
                Items.Add(destination.Content.Item);
                ResourceMetas.Add(destination.Content.Item.CoverImage);
                Resources.Add(destination.Content.Item.CoverImage.Resource);
            }

            foreach (City city in Cities)
            {
                Items.Add(city.Item);
                ResourceMetas.Add(city.Item.CoverImage);
                Resources.Add(city.Item.CoverImage.Resource);
            }

            modelBuilder.Entity<IdentityRole>().HasData(IdentityRoles);
            modelBuilder.Entity<AppUser>().HasData(Users);
            modelBuilder.Entity<Item>().HasData(Items);
            modelBuilder.Entity<City>().HasData(Cities);
            modelBuilder.Entity<Content>().HasData(Content);
            modelBuilder.Entity<Activity>().HasData(Activities);
            modelBuilder.Entity<Destination>().HasData(Destinations);
            modelBuilder.Entity<ResourceMeta>().HasData(ResourceMetas);
            modelBuilder.Entity<Resource>().HasData(Resources);
            modelBuilder.Entity<Reward>().HasData(Rewards);
            modelBuilder.Entity<Shortlist>().HasData(Shortlists);

            // Junctions
            modelBuilder.Entity<CityActivity>().HasData(CityActivities);
            modelBuilder.Entity<CityDestination>().HasData(CityDestinations);
            modelBuilder.Entity<ContentResourceMeta>().HasData(ContentResourceMetas);
            modelBuilder.Entity<History>().HasData(Histories);
            modelBuilder.Entity<ShortlistContent>().HasData(ShortlistContent);
            modelBuilder.Entity<UserReward>().HasData(UserRewards);
            modelBuilder.Entity<UserShortlist>().HasData(UserShortlists);
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

            Join(users[0], IdentityRoles[1]);
            Join(users[1], IdentityRoles[0]);
            Join(users[2], IdentityRoles[0]);

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

            Join(Users[0], shortlists[0]);
            Join(Users[0], shortlists[1]);
            Join(Users[2], shortlists[2]);

            return shortlists;
        }

        public List<City> GenerateCities()
        {
            var cities = new List<City>()
            {
                DataFactory.CreateCity("Brisbane", "Captial of QLD", "Australia", "Brisbane.jfif", "Brisbane"),
                DataFactory.CreateCity("Sydney", "Slow fall into hell", "Australia", "Sydney.jfif", "Sydney"),
                DataFactory.CreateCity("Melbourne", "Land of the dead", "Australia", "Melbourne.jfif", "Melbourne"),
            };

            return cities;
        }

        public List<Activity> GenerateActivities()
        {
            var activities = new List<Activity>()
            {
                DataFactory.CreateActivity("Pub Crawl", "Tour Brisbanes best bars and clubs in a night of fun",
                    "PubCrawl.jfif", "PubCrawl",3, 5, 5),
                DataFactory.CreateActivity("Uni tour", "Visit Brisbanes best universities",
                    "UniTour.jfif", "UniTour", 3, 5, 5),
                DataFactory.CreateActivity("Catch Covid", "Do the world a favor and remove yourself from this earth",
                    "Covid.png", "Covid", 5, 5, 5)
            };

            // CityActivities
            Join(Cities[0], activities[0]);
            Join(Cities[0], activities[1]);
            Join(Cities[3], activities[2]);

            // ShortlistContent
            Join(Shortlists[1], activities[0].Content, 1);
            Join(Shortlists[1], activities[1].Content, 2);
            Join(Shortlists[2], activities[2].Content, 1);

            return activities;
        }

        public List<Destination> GenerateDestinations()
        {
            var destinations = new List<Destination>()
            {
                DataFactory.CreateDestination("UQ", "The best uni in brisbane",
                    "UQ.jfif", "UQ", 5, 5, 5),
                DataFactory.CreateDestination("South Brisbane Cemetery", "Super spooooky at night",
                    "SouthBrisbaneCemetery.jfif", "SouthBrisbaneCemetery", 4, 3, 5),
                DataFactory.CreateDestination("Sydney Opera House", "Australia's most famouse landmark",
                    "OperaHouse.jfif", "OperaHouse", 5, 5, 5)
            };

            // CityActivities
            Join(Cities[0], destinations[0]);
            Join(Cities[0], destinations[1]);
            Join(Cities[3], destinations[2]);

            // ShortlistContent
            Join(Shortlists[0], destinations[1].Content, 2);
            Join(Shortlists[2], destinations[2].Content, 1);

            return destinations;
        }

        public List<Reward> GenerateRewards()
        {
            var rewards = new List<Reward>()
            {
                DataFactory.CreateReward("Covid Bonus", "Buy 1 get 2 FREE")
            };

            Join(Users[1], rewards[0]);

            return rewards;
        }

        public void Join(AppUser user, IdentityRole role)
        {
            IdentityUserRoles.Add(DataFactory.CreateIdentityUserRole(user, role));
        }

        public void Join(City city, Activity activity)
        {
            CityActivities.Add(DataFactory.CreateCityActivity(city, activity));
        }

        public void Join(City city, Destination destination)
        {
            CityDestinations.Add(DataFactory.CreateCityDestination(city, destination));
        }

        public void Join(Content content, ResourceMeta resourceMeta, int num)
        {
            ContentResourceMetas.Add(DataFactory.CreateContentResourceMeta(content, resourceMeta, num));
        }

        public void Join(Content content, AppUser user)
        {
            Histories.Add(DataFactory.CreateHistory(user, content));
        }

        public void Join(Shortlist shortlist, Content content, int num)
        {
            ShortlistContent.Add(DataFactory.CreateShortlistContent(shortlist, content, num));
        }

        public void Join(AppUser user, Shortlist shortlist)
        {
            UserShortlists.Add(DataFactory.CreateUserShortlist(user, shortlist));
        }

        public void Join(AppUser user, Reward reward)
        {
            UserRewards.Add(DataFactory.CreateUserReward(user, reward));
        }
    }
}