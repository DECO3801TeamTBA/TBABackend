using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;

namespace WanderListAPI.Data
{
    public class WanderListDbContext : IdentityDbContext<ApplicationUser>
    {

        //Add DbSet<Entity> here
        public DbSet<Activity> Activity { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<ResourceMeta> ResourceMeta { get; set; }
        //public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Reward> Reward { get; set; }
        public DbSet<Shortlist> Shortlist { get; set; }
        public DbSet<ShortlistContent> ShortlistContent { get; set; }
        public DbSet<UserReward> UserReward { get; set; }

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

            //Add fluentAPI calls here. Not necessary atm I think, since we're using DataAnnotations (see Models)
            //Additionally, some properties do not even require DataAnnotations (see EF Core docs)

            //Composite Keys here
            modelBuilder.Entity<History>().HasKey(hist => new { hist.ContentId, hist.UserId });
            modelBuilder.Entity<ShortlistContent>().HasKey(slc => new { slc.ContentId, slc.ShortlistId });
            modelBuilder.Entity<UserReward>().HasKey(ur => new { ur.UserId, ur.RewardId });


            //Generate Data
            var seed = new DataFactory();

            // User stuff
            IdentityRole adminRole = seed.CreateIdentityRole("Admin");
            IdentityRole userRole = seed.CreateIdentityRole("User");
            ApplicationUser user = seed.CreateApplicationUser();

            modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            modelBuilder.Entity<IdentityRole>().HasData(userRole);
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = userRole.Id,
                UserId = user.Id
            });

            // Content stuff
            Content activityContent = seed.CreateContent();
            Content destinationContent = seed.CreateContent();
            Activity activity = seed.CreateActivity(activityContent);
            Destination destination = seed.CreateDestination(destinationContent);

            modelBuilder.Entity<Content>().HasData(activityContent);
            modelBuilder.Entity<Content>().HasData(destinationContent);
            modelBuilder.Entity<Activity>().HasData(activity);
            modelBuilder.Entity<Destination>().HasData(destination);

            // Shortlist stuff
            Shortlist shortlist = seed.CreateShortlist(user.Id);

            modelBuilder.Entity<Shortlist>().HasData(shortlist);
            modelBuilder.Entity<ShortlistContent>().HasData(seed.CreateShortlistContent(shortlist.ShortlistId,
                activity.ActivityId, 0));
            modelBuilder.Entity<ShortlistContent>().HasData(seed.CreateShortlistContent(shortlist.ShortlistId,
                destination.DestinationId, 0));

            // Reward stuff
            var reward = seed.CreateReward();

            modelBuilder.Entity<Reward>().HasData(reward);
            modelBuilder.Entity<UserReward>().HasData(seed.CreateUserReward(user.Id, reward.RewardId));

            // History stuff
            modelBuilder.Entity<History>().HasData(seed.CreateHistory(user.Id, activity.ActivityId));
            modelBuilder.Entity<History>().HasData(seed.CreateHistory(user.Id, destination.DestinationId));
        }
    }
}
