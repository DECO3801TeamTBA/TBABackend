using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Data
{
    public class WanderListDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Fill in with appropriate connection string later!
            optionsBuilder.UseMySQL("server=localhost;database=library;user=user;password=password");
        }
    }
}
