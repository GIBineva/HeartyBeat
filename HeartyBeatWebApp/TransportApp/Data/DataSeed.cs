using HeartyBeatApp.Data;
using HeartyBeatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeartyBeat.Data
{
    public static class DataSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await SeedRewardsAsync(context);
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                await SeedUsersAsync(roleManager, userManager);
            }
        }

        private static async Task SeedUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            await SeedRoles(roleManager);
            await SeedAdminAsync(userManager);
            await SeedRegularUser(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                string[] roles = new string[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = role, NormalizedName = role.ToUpper() });
                }
            }
        }

        private static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var adminUsername = "admin@admin.com";
            var user = await userManager.FindByNameAsync(adminUsername);
            if (user == null)
            {
                var admin = new AppUser()
                {
                    UserName = adminUsername,
                    Email = adminUsername
                };

                await userManager.CreateAsync(admin, "Pass123@");

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        private static async Task SeedRegularUser(UserManager<AppUser> userManager)
        {
            var username = "user@user.com";
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                var account = new AppUser()
                {
                    UserName = username,
                    Email = username
                };

                await userManager.CreateAsync(account, "User123@");

                await userManager.AddToRoleAsync(account, "User");
            }
        }



        private static async Task SeedRewardsAsync(ApplicationDbContext context)
        {

            if (context.Reward.Any())
            {
                return;
            }

            context.Reward.AddRange(
                new Reward { Message = "Great job! Keep up the good work!", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "You did it! Stay strong!", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "Fantastic effort! Keep going!", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "Awesome! You're doing great!", ImageUrl = "/images/HeartCat.jpg" },
                new Reward { Message = "Excellent! Keep pushing forward!", ImageUrl = "/images/HeartCat.jpg" }
            );

            await context.SaveChangesAsync();
        }


    }
}
