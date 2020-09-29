using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;
using WanderListAPI.Models.Junctions;

namespace WanderListAPI.Data
{
    public class WanderListDbContext : IdentityDbContext<AppUser>
    {

        //Add DbSet<Entity> here
        public DbSet<Activity> Activity { get; set; }
        public DbSet<AppUser> ApplicationUser { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CityActivity> CityActivity { get; set; }
        public DbSet<CityDestination> CityDestination { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<ContentResourceMeta> ContentResourceMeta { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<ResourceMeta> ResourceMeta { get; set; }
        public DbSet<Reward> Reward { get; set; }
        public DbSet<Shortlist> Shortlist { get; set; }
        public DbSet<ShortlistContent> ShortlistContent { get; set; }
        public DbSet<UserReward> UserReward { get; set; }
        public DbSet<UserShortlist> UserShortlist { get; set; }

        public WanderListDbContext()
        {
            //empty constructor
        }

        public WanderListDbContext(DbContextOptions<WanderListDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Fill in with appropriate connection string later!
            //optionsBuilder.UseMySql("server=localhost;database=WanderList;user=TBADev;password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call base class OnModelCreating to setup
            base.OnModelCreating(modelBuilder);

            //Composite Keys here
            modelBuilder.Entity<CityActivity>().HasKey(ccont => new { ccont.CityId, ccont.ContentId });
            modelBuilder.Entity<ContentResourceMeta>().HasKey(crmet => new { crmet.ContentId, crmet.ResourceMetaId });
            modelBuilder.Entity<History>().HasKey(hist => new { hist.ContentId, hist.UserId });
            modelBuilder.Entity<ShortlistContent>().HasKey(slc => new { slc.ContentId, slc.ShortlistId });
            modelBuilder.Entity<UserReward>().HasKey(ur => new { ur.UserId, ur.RewardId });
            modelBuilder.Entity<UserShortlist>().HasKey(us => new { us.UserId, us.ShortlistId });


            //Generate Data

            //// User stuff
            //IdentityRole adminRole = DataSeed.CreateIdentityRole("Admin");
            //IdentityRole userRole = DataSeed.CreateIdentityRole("User");
            //AppUser user = DataSeed.CreateApplicationUser();

            //modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            //modelBuilder.Entity<IdentityRole>().HasData(userRole);
            //modelBuilder.Entity<AppUser>().HasData(user);
            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = userRole.Id,
            //    UserId = user.Id
            //});

            ////Make a resource to be used for cover images
            //var (resource, resourceMeta) = DataSeed.CreateResourceWithMeta();
            //modelBuilder.Entity<Resource>().HasData(resource); //is this sufficient?
            //modelBuilder.Entity<ResourceMeta>().HasData(resourceMeta);

            ////First make a city
            //var (city, cityItem) = DataSeed.CreateCityWithItem(resourceMeta.ResourceMetaId);
            //modelBuilder.Entity<Item>().HasData(cityItem);
            //modelBuilder.Entity<City>().HasData(city);

            ////make a destination content with city connected
            //var (destination, destContent, destCity, destinationItem) =
            //    DataSeed.CreateDestinationWithContentAndItem(
            //        resourceMeta.ResourceMetaId,
            //        city.CityId
            //    );
            //var destinationResource = DataSeed.CreateContentResourceMeta
            //    (resourceMeta.ResourceMetaId, destination.DestinationId, 0);
            //modelBuilder.Entity<Item>().HasData(destinationItem);
            //modelBuilder.Entity<Content>().HasData(destContent);
            //modelBuilder.Entity<Destination>().HasData(destination);
            //modelBuilder.Entity<CityActivity>().HasData(destCity);
            //modelBuilder.Entity<ContentResourceMeta>().HasData(destinationResource);


            //// make an activity content with city connected
            //var (activity, actContent, actCity, activityItem) =
            //    DataSeed.CreateActivityWithContentAndItem(
            //        resourceMeta.ResourceMetaId,
            //        city.CityId
            //    );
            //var activityResource = DataSeed.CreateContentResourceMeta
            //    (resourceMeta.ResourceMetaId, activity.ActivityId, 0);
            //modelBuilder.Entity<Item>().HasData(activityItem);
            //modelBuilder.Entity<Content>().HasData(actContent);
            //modelBuilder.Entity<Activity>().HasData(activity);
            //modelBuilder.Entity<CityActivity>().HasData(actCity);
            //modelBuilder.Entity<ContentResourceMeta>().HasData(activityResource);
            //// Shortlist stuff
            //var (shortlist, usershortlist) = DataSeed.CreateShortlist(user.Id);
            //modelBuilder.Entity<Shortlist>().HasData(shortlist);
            //modelBuilder.Entity<UserShortlist>().HasData(usershortlist);
            //modelBuilder.Entity<ShortlistContent>().HasData(
            //    DataSeed.CreateShortlistContent(shortlist.ShortlistId, activity.ActivityId, 1));
            //modelBuilder.Entity<ShortlistContent>().HasData(
            //    DataSeed.CreateShortlistContent(shortlist.ShortlistId, destination.DestinationId, 2));

            //// Reward stuff
            //var reward = DataSeed.CreateReward();
            //modelBuilder.Entity<Reward>().HasData(reward);
            //modelBuilder.Entity<UserReward>().HasData(DataSeed.CreateUserReward(user.Id, reward.RewardId));

            //// History stuff
            //modelBuilder.Entity<History>().HasData(DataSeed.CreateHistory(user.Id, activity.ActivityId));
            //modelBuilder.Entity<History>().HasData(DataSeed.CreateHistory(user.Id, destination.DestinationId));
        }
    }
}
