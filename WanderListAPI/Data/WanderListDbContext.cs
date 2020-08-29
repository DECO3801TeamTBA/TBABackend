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
            modelBuilder.Entity<History>().HasKey(hist => new { hist.ContentId, hist.UserId });

            //create some Ids
            string userId = Guid.NewGuid().ToString();
            string roleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "Admin"
            });

            var user = new ApplicationUser()
            {
                FirstName = "JoeyJojo",
                LastName = "Shabadoo",
                Id = userId,
                UserName = "wanderuser",
                Email = "fake@fake.com",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "1234"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            });

            //Add to WanderUser table too, we can probably just remove WanderUser and house everything in ApplicationUser?
            //modelBuilder.Entity<WanderUser>().HasData(user);

            var contentId = Guid.NewGuid();
            modelBuilder.Entity<Content>().HasData(new Content
            {
                ContentId = contentId,
                Description = "fake",
                Address = "fake",
                Name = "Fakorama",
                Lattitude = 15.51M,
                Longitude = 45.15M,
                Website = "www.fake.com",
                Capacity = 200
            });

            modelBuilder.Entity<History>().HasData(new History()
            {
                UserId = userId,
                ContentId = contentId,
                Date = DateTime.Now
            });



            //MySQL issues, found on stack overflow here:
            //https://stackoverflow.com/questions/49573740/identity-and-mysql-in-code-first-specified-key-was-too-long-max-key-length-is

            // We are using int here because of the change on the PK

        }
    }
}
