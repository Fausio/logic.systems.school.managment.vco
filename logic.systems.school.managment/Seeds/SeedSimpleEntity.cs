using logic.systems.school.managment.Data;
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

            if ( await db.SimpleEntitys.FirstOrDefaultAsync(x => x.type == "Gender") is null )
            {

            }
        }
    }
}
