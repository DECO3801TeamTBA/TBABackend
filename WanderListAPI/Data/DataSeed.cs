using Microsoft.AspNetCore.Identity;
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
    public static class DataSeed
    {
        public static List<IdentityRole> CreateIdentityRole()
        {
            var identityRoles = new List<IdentityRole>()
            {
                DataFactory.CreateIdentityRole("User"),
                DataFactory.CreateIdentityRole("Admin")
            };

            return identityRoles;
        }
    }
}