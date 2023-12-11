using logic.systems.school.managment.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedUsers
    {
        public static async Task Run()
        {
           
        }

        private static async Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            // Criar funções (roles) se não existirem
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
