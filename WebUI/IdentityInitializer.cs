using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace WebUI
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole {Name = "Admin"});
            }

            var memberRole = await roleManager.FindByNameAsync("Member");
            if (memberRole == null)
            {
                await roleManager.CreateAsync(new AppRole {Name = "Member"});
            }


            var adminUser = await userManager.FindByNameAsync("Samet");
            if (adminUser == null)
            {
                AppUser user = new AppUser
                {
                    FirstName = "Samet",
                    LastName = "İrkören",
                    UserName = "Samet",
                    Email = "sametirkoren@gmail.com",
                    EmailConfirmed = true

                };
                await userManager.CreateAsync(user,"Aqswde123!.");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
