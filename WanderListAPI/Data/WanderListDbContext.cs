using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;

namespace WanderListAPI.Data
{
    public class WanderListDbContext : DbContext
    {

        //Add DbSet<Entity> here
        public DbSet<Content> Content { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<ResourceMeta> ResourceMeta { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<WanderUser> WanderUser { get; set; }
        public DbSet<History> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Fill in with appropriate connection string later!
            optionsBuilder.UseMySQL("server=localhost;database=library;user=user;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //call base class OnModelCreating to setup
            base.OnModelCreating(modelBuilder);

            //Add fluentAPI calls here. Not necessary atm I think, since we're using DataAnnotations (see Models)
            //Additionally, some properties do not even require DataAnnotations (see EF Core docs)


        }
    }
}
