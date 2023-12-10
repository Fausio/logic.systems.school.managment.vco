using logic.systems.school.managment.Data;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedSimpleEntity
    {
        public static async Task Run()
        {
            await Seed();
        }

        private static async Task Seed()
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            if (await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == "SchoolLevel") is null)
            {
                List<SimpleEntity> schoolLevels = new List<SimpleEntity>
                {
                new SimpleEntity { Type = "SchoolLevel", Description = "Pré-escola A" },
                new SimpleEntity { Type = "SchoolLevel", Description = "Pré-escola B" },
                new SimpleEntity { Type = "SchoolLevel", Description = "Pré-escola C" },
                new SimpleEntity { Type = "SchoolLevel", Description = "1ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "2ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "3ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "4ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "5ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "6ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "7ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "8ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "9ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "10ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "11ª classe" },
                new SimpleEntity { Type = "SchoolLevel", Description = "12ª classe" }
                };

                await db.AddRangeAsync(schoolLevels);
                await db.SaveChangesAsync();

            }
        }
    }
}
