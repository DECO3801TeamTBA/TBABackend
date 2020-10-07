﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<QR> QR { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<ResourceMeta> ResourceMeta { get; set; }
        public DbSet<Reward> Reward { get; set; }
        public DbSet<Shortlist> Shortlist { get; set; }
        public DbSet<ShortlistContent> ShortlistContent { get; set; }
        public DbSet<UserReward> UserReward { get; set; }
        public DbSet<UserShortlist> UserShortlist { get; set; }

        public WanderListDbContext()
        {
        }

        public WanderListDbContext(
            DbContextOptions<WanderListDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            //Fill in with appropriate connection string later!
            //optionsBuilder.UseMySql("server=localhost;database=WanderList;user=TBADev;password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call base class OnModelCreating to setup
            base.OnModelCreating(modelBuilder);

            //Composite Keys here
            modelBuilder.Entity<CityActivity>().HasKey(ccont =>
                new {ccont.CityId, ccont.ActivityId});
            modelBuilder.Entity<CityDestination>().HasKey(ccont =>
                new {ccont.CityId, ccont.DestinationId});
            modelBuilder.Entity<ContentResourceMeta>().HasKey(crmet =>
                new {crmet.ContentId, crmet.ResourceMetaId});
            modelBuilder.Entity<History>()
                .HasKey(hist => new {hist.ContentId, hist.UserId});
            modelBuilder.Entity<ShortlistContent>().HasKey(slc =>
                new {slc.ContentId, slc.ShortlistId});
            modelBuilder.Entity<UserReward>()
                .HasKey(ur => new {ur.UserId, ur.RewardId});
            modelBuilder.Entity<UserShortlist>()
                .HasKey(us => new {us.UserId, us.ShortlistId});

            //Generate Data
            var seed = new DataSeed();
            seed.AddData(modelBuilder);
        }
    }
}