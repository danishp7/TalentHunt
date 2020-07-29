using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Data;
using TalentHunt.Models;

namespace TalentHunt.Helpers
{
    public static class SeedData
    {
        public static async Task SeedUserData(UserManager<User> userManager, TalentHuntContext ctx,
                                        RoleManager<IdentityRole> roleManager)
        {
            var role = new IdentityRole("Hr");

            if (!ctx.Roles.Any())
            {
                await roleManager.CreateAsync(role);
            }

            if (!ctx.Users.Any())
            {
                string name = "saad_hr@talenthunt.com";
                User user = new User
                {
                    UserName = name,
                    Email = name,
                    NormalizedUserName = name.ToUpper().Normalize(),
                    NormalizedEmail = name.ToUpper().Normalize()
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Hr");
            }
        }
    }
}
