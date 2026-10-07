using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Repository.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            string superAdminEmail,
            string adminEmail,
            string password)
        {
            foreach (var role in Roles.All)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            await EnsureUserAsync(userManager, superAdminEmail, "Super", "Admin", password, Roles.SuperAdmin);
            await EnsureUserAsync(userManager, adminEmail, "Site", "Admin", password, Roles.Admin);
        }

        private static async Task EnsureUserAsync(
            UserManager<AppUser> userManager,
            string email,
            string name,
            string surname,
            string password,
            string role)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;

            email = email.Trim();
            if (await userManager.FindByEmailAsync(email) is not null)
                return;

            var user = new AppUser
            {
                Name = name,
                Surname = surname,
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            var created = await userManager.CreateAsync(user, password);
            if (created.Succeeded)
                await userManager.AddToRoleAsync(user, role);
        }
    }
}
