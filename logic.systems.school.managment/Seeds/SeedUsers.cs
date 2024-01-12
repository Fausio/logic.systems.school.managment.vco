using DocumentFormat.OpenXml.InkML;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedUsers
    {
        public async static Task Run(IServiceProvider serviceProvider)
        {

            var db = serviceProvider.GetService<ApplicationDbContext>();

            string[] roles = new string[] { "Administrator".ToUpper(), "employee".ToUpper() };
            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(db);

                if (!await db.Roles.AnyAsync(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            var user = new IdentityUser
            {

                Email = "admin@Kalimany.com",
                NormalizedEmail = "admin@Kalimany.com",
                UserName = "admin",
                NormalizedUserName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!await db.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "admin1234");
                user.PasswordHash = hashed;

                var userStore = new UserStore<IdentityUser>(db);
                var result = userStore.CreateAsync(user);
            }

            await AssignRoles(serviceProvider, user.Email, roles);
            await db.SaveChangesAsync();
        }

        public async static Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<IdentityUser> _userManager = services.GetService<UserManager<IdentityUser>>();
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }
    }
}
