using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        private List<ResourceMeta> resourceMetas;
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
            resourceMetas = new List<ResourceMeta>();
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

            foreach (AppUser user in users.Values)
            {
                resourceMetas.Add(user.ProfilePic);
                DataFactory.Clean(user);
            }

            foreach (Activity activity in activities.Values)
            {
                content.Add(activity.Content);
                items.Add(activity.Content.Item);
                DataFactory.Clean(activity);
            }

            foreach (Destination destination in destinations.Values)
            {
                content.Add(destination.Content);
                items.Add(destination.Content.Item);
                DataFactory.Clean(destination);
            }

            foreach (City city in cities.Values)
            {
                items.Add(city.Item);
                resourceMetas.Add(city.Item.CoverImage);
                DataFactory.Clean(city);
            }

            foreach (Reward reward in rewards.Values)
            {
                resourceMetas.Add(reward.CoverImage);
                DataFactory.Clean(reward);
            }

            foreach (ResourceMeta resource in resourceMetas)
            {
                resources.Add(resource.Resource);
                DataFactory.Clean(resource);
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
                {"Shaggy", DataFactory.CreateUser("Norville", "Rogers", "Shaggy", 100)},
                {"Scooby", DataFactory.CreateUser("Scoobert", "Doo", "Scooby", 500)},
                {"Velma", DataFactory.CreateUser("Velma", "Dinkley", "Velma", 400)},
                {"Fred", DataFactory.CreateUser("Fred", "Jones", "Fred", 375)},
                {"Daphne", DataFactory.CreateUser("Daphne", "Blakeo", "Daphne", 400)},
            };

            users["Shaggy"].ProfilePic = DataFactory.CreateResourceMeta("harold.jfif");
            users["Velma"].ProfilePic = DataFactory.CreateResourceMeta("Velma.jfif");

            Join(users["Shaggy"], identityRoles["User"]);
            Join(users["Scooby"], identityRoles["User"]);
            Join(users["Velma"], identityRoles["User"]);
            Join(users["Fred"], identityRoles["User"]);
            Join(users["Daphne"], identityRoles["User"]);

            return users;
        }

        public Dictionary<string, Shortlist> GenerateShortlists(Dictionary<string, AppUser> users)
        {
            var shortlists = new Dictionary<string, Shortlist>()
            {
                {"Brisbane Holiday", DataFactory.CreateShortlist("Brisbane Holiday", users["Velma"]) },
                {"Bucket List", DataFactory.CreateShortlist("Bucket List", users["Velma"]) },
                {"Shag Spots", DataFactory.CreateShortlist("Shag Spots", users["Shaggy"]) },
                {"Ghost Sightings", DataFactory.CreateShortlist("Ghost Sightings", users["Shaggy"]) }
            };

            return shortlists;
        }

        public Dictionary<string, City> GenerateCities()
        {
            return new Dictionary<string, City>()
            {
                {"Brisbane", DataFactory.CreateCity("Brisbane", "Brisbane is the capital of Queensland, " +
                "and the third most populous city in Australia.",
                    "Australia", "nDHlEG48b-M", -27.46794, 153.02809, "Brisbane.jfif")},
                {"Sydney", DataFactory.CreateCity("Sydney", "Sydney is the state capital of New South " +
                "Wales and the most populous city in Australia and Oceania.",
                    "Australia", "Yc7r_bbt00M", -33.86785, 151.20732, "Sydney.jfif")},
                {"Melbourne", DataFactory.CreateCity("Melbourne", "Melbourne is the capital of Victoria, " +
                "and the second-most populous city in Australia and Oceania.",
                    "Australia", "Rzn5WGnS350", -37.814, 144.96332, "Melbourne.jfif")}
            };
        }

        public Dictionary<string, Activity> GenerateActivities(Dictionary<string, City> cities, 
            Dictionary<string, Shortlist> shortlists, Dictionary<string, AppUser> users)
        {
            var activities = new Dictionary<string, Activity>()
            {
                // Brisbane
                {"Pub Crawl", DataFactory.CreateActivity("Pub Crawl",
                    "Tour Brisbanes best bars and clubs in a night of fun",
                    "PubCrawl.jpg", 3, 5, 5, -27.470568, 153.024866, cities["Brisbane"])},
                {"Uni tour", DataFactory.CreateActivity("Uni tour",
                    "Visit Brisbanes best universities",
                    "UniTour.jpg", 3, 5, 5, -27.477119, 153.028372, cities["Brisbane"])},
                {"Segway", DataFactory.CreateActivity("Brisbane Segway Sightseeing Tour",
                    "Make the most of your vacation time in Brisbane by embarking on a Segway " +
                    "tour of the riverside city. Zooming around on this two-wheeled, self-balancing, " +
                    "electric scooter allows you to cover much more ground",
                    "Segway.jpg", 3, 5, 4, -27.476328, 153.009019, cities["Brisbane"])},
                {"Scavenger", DataFactory.CreateActivity("Brisbane Scavenger Hunt",
                    "Walk to all the best landmarks and hidden gems, answering trivia " +
                    "questions and solving challenges. Work with your team or compete against " +
                    "them, as you learn new facts and create memorable experiences on this 2h activity. ",
                    "Scavenger.jpg", 3, 5, 5, -27.465718, 153.024058, cities["Brisbane"])},
                {"Rock Climbing", DataFactory.CreateActivity("Brisbane Rock Climbing",
                    "Kangaroo Point cliff face is a unique sight in the heart of Brisbane, " +
                    "climb the cliffs whilst they are lit up in the evening. The urban cliff " +
                    "offers a unique rock climbing experience, allowing beginners and the experienced " +
                    "to be challenged by its various routes.",
                    "Rock Climbing.jpg", 3, 5, 5, -27.477733, 153.034482, cities["Brisbane"])},
                {"River Cruise", DataFactory.CreateActivity("Twilight River Cruise",
                    "The only way to see the Brisbane River. The back drop of the city underlights " +
                    "with these reflecting off the river create an amazing ambiance.",
                    "River Cruise.jpg", 3, 5, 5, -27.470408, 153.018872, cities["Brisbane"])},

                // Sydney
                {"Whale-Watching", DataFactory.CreateActivity("Sydney Whale-Watching by Speed Boat",
                "Get the chance to spot humpback whales right outside of Sydney on this speed boat tour " +
                "from Circular Quay or Manly Wharf.", "Balloon Flight.jpg", 5, 5, 5, -33.856789, 151.209252, 
                cities["Sydney"])},


                // Melbourne
                {"Balloon Flight", DataFactory.CreateActivity("Yarra Valley Balloon Flight at Sunrise", 
                "In an intimate group limited to 16 people, float over Yarra Valley vineyards at sunrise, " +
                "when the landscapes look most magical.", "Balloon Flight.jpg", 3, 5, 5, -37.631935, 145.400453, 
                cities["Melbourne"])},
                {"Art Gallery", DataFactory.CreateActivity("ArtVo Immersive Gallery Experience", "ArtVo is an art gallery with a difference—this immersive " +
                "art space encourages people to touch, play, and interact with the art, and there are 11 themed zones " +
                "to explore.", "Art Gallery.jpg", 5, 5, 3, -37.812648, 144.937671,
                cities["Melbourne"])},
                {"Kayak", DataFactory.CreateActivity("Melbourne City Afternoon Kayak Tour",
                "Explore Melbourne from the river at your own pace and without anyone getting " +
                "in the way on this afternoon kayaking tour.", "Balloon Flight.jpg", 5, 4, 5, -37.820381, 144.958287,
                cities["Melbourne"])},
            };

            foreach (Activity activity in activities.Values)
            {
                int num = 0;
                foreach (ResourceMeta image in DataFactory.CreateImageGallery(activity.Content.Item))
                {
                    Join(activity.Content, image, num++);
                }
            }


            // ShortlistContent
            Join(shortlists["Brisbane Holiday"], activities["Pub Crawl"].Content, 1);
            Join(shortlists["Brisbane Holiday"], activities["Uni tour"].Content, 2);

            Join(shortlists["Bucket List"], activities["Whale-Watching"].Content, 1);
            Join(shortlists["Shag Spots"], activities["Balloon Flight"].Content, 2);

            // History
            Join(users["Shaggy"], activities["Pub Crawl"].Content);
            Join(users["Shaggy"], activities["Uni tour"].Content);

            Join(users["Scooby"], activities["Pub Crawl"].Content);

            Join(users["Velma"], activities["Art Gallery"].Content);
            Join(users["Velma"], activities["Uni tour"].Content);

            return activities;
        }

        public Dictionary<string, Destination> GenerateDestinations(Dictionary<string, City> cities,
            Dictionary<string, Shortlist> shortlists, Dictionary<string, AppUser> users)
        {
            var destinations = new Dictionary<string, Destination>()
            {
                // Brisbane
                {"UQ", DataFactory.CreateDestination("UQ", "The best uni in brisbane",
                    "UQ.jpg", 5, 5, 5,-27.497408, 153.013680, cities["Brisbane"])},
                {"South Brisbane Cemetery", DataFactory.CreateDestination("South Brisbane Cemetery",
                    "Super spooooky at night",
                    "SouthBrisbaneCemetery.jpg", 4, 3, 5, -27.498973, 153.027120, cities["Brisbane"])},
                {"Rainforest", DataFactory.CreateDestination("Springbrook and Tamborine Rainforest", 
                "Explore the mountains, caves, and waterfalls of the Gold Coast Hinterlands. Admire " +
                "the Natural Bridge and trek to Cave Creek waterfall in the Springbrook National Park.",
                "Rainforest.jpg", 5, 5, 5, -28.209280, 153.270175, cities["Brisbane"])},
                {"Australia Zoo", DataFactory.CreateDestination("Australia Zoo",
                "This is the perfect pick for animal lovers! Visit the world-renowned Australia Zoo—also " +
                "known as “The Home of the Crocodile Hunter” and owned by Steve Irwin’s widow Terri Irwin.",
                "Australia Zoo.jpg", 4, 5, 3, -26.835488, 152.963134, cities["Brisbane"])},
                {"Stradbroke", DataFactory.CreateDestination("North Stradbroke Island",
                "A beautiful tropical island located west of brisbane, with many beautiful white beaches " +
                "and sea views. Dolphins, turtles and many other marine and coastal life are frequently sighted here.",
                "Stradbroke.jpg", 4, 4, 5, -27.509375, 153.468471, cities["Brisbane"])},
                {"Lone Pine", DataFactory.CreateDestination("Lone Pine Koala Sanctuary",
                " Meet a koala, hand-feed kangaroos and engage with a large variety of Australian wildlife in " +
                "Lone Pine's beautiful, natural settings. Guests experience happy, healthy animals and engaged " +
                "staff, as well as the opportunity to support conservation and enjoy educational opportunities.",
                "Lone Pine.jpg", 5, 5, 5, -27.533553, 152.968783, cities["Brisbane"])},

                // Sydney
                {"Sydney Opera House", DataFactory.CreateDestination("Sydney Opera House",
                    "Australia's most famouse landmark",
                    "OperaHouse.jpg", 3, 5, 3, -33.856651, 151.215276, cities["Sydney"])},
                {"Chinese Garden", DataFactory.CreateDestination("Chinese Garden of Friendship",
                    "Explore the lush plant life, hidden pagodas, and colorful statues at your " +
                    "own speed, or join one of three informative tours that run during the day at " +
                    "no extra cost.",
                    "OperaHouse.jpg", 5, 5, 4, -33.876274, 151.202802, cities["Sydney"])},
                {"Aquarium", DataFactory.CreateDestination("SEA LIFE Sydney Aquarium",
                    "See the more than 13,000 aquatic life forms in the 14 themed areas.",
                    "Aquarium.jpg", 2, 5, 3, -33.869350, 151.202192, cities["Sydney"])},
                
                // Melbourne
                {"Chinatown", DataFactory.CreateDestination("Melbourne Chinatown",
                    "China Town’s great for Yum Cha, Chinese Food and a visit to Dessert Story, " +
                    "they have the best Taiwanese desserts!",
                    "Chinatown.jpg", 4, 5, 5, -37.811280, 144.968809, cities["Melbourne"])}
            };

            foreach (Destination destination in destinations.Values)
            {
                int num = 0;
                foreach (ResourceMeta image in DataFactory.CreateImageGallery(destination.Content.Item))
                {
                    Join(destination.Content, image, num++);
                }
            }

            // ShortlistContent
            Join(shortlists["Brisbane Holiday"], destinations["UQ"].Content, 3);
            Join(shortlists["Ghost Sightings"], destinations["South Brisbane Cemetery"].Content, 1);

            Join(shortlists["Bucket List"], destinations["Sydney Opera House"].Content, 2);
            Join(shortlists["Shag Spots"], destinations["Chinese Garden"].Content, 2);
            Join(shortlists["Shag Spots"], destinations["South Brisbane Cemetery"].Content, 3);

            // History
            Join(users["Shaggy"], destinations["South Brisbane Cemetery"].Content);

            Join(users["Scooby"], destinations["South Brisbane Cemetery"].Content);

            Join(users["Velma"], destinations["UQ"].Content);
            Join(users["Velma"], destinations["South Brisbane Cemetery"].Content);

            return destinations;
        }

        public Dictionary<string, Reward> GenerateRewards(Dictionary<string, City> cities)
        {
            var rewards = new Dictionary<string, Reward>()
            {
                {"Uni Tour Discount", DataFactory.CreateReward("Uni Tour Discount",
                    "15% Off your next tour", cities["Brisbane"], 1, "Uni Tour.jfif")},
                {"Drink Discount", DataFactory.CreateReward("Drink Discount",
                    "$5 OFF a jug of beer with any meal purchase", cities["Brisbane"], 4, "Beer.jfif")},

                {"Sydney Aquarium Voucher", DataFactory.CreateReward("Sydney Aquarium Voucher", 
                    "5% off your next ticket", cities["Sydney"], 1, "Sydney Aquarium.jfif")},
                {"Free tour", DataFactory.CreateReward("Free tour of Chinese Garden of Friendship",
                    "Free tour with any ticket purchase", cities["Sydney"], 0, "Chinese Garden.jfif")},

                {"Kayak deal", DataFactory.CreateReward("Save when you bring a Friend",
                    "1/2 price for the scond person for your Melbourne City Afternoon Kayak Tour", 
                    cities["Melbourne"], 1, "Kayak.jfif")}
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
            resourceMetas.Add(resourceMeta);
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