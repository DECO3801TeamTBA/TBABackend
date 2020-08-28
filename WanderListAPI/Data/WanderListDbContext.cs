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
        public DbSet<Content> Content { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<ResourceMeta> ResourceMeta { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<WanderUser> WanderUser { get; set; }
        public DbSet<History> History { get; set; }

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
            modelBuilder.Entity<History>().HasKey(hist => new { hist.ContentId, hist.WanderUserId });

            modelBuilder.Entity<ApplicationUser>()
                .Property(user => user.Id)
                .HasMaxLength(36);

            //MySQL issues, found on stack overflow here:
            //https://stackoverflow.com/questions/49573740/identity-and-mysql-in-code-first-specified-key-was-too-long-max-key-length-is

            // We are using int here because of the change on the PK

        }
    }
}
