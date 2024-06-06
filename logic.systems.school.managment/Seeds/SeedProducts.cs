using logic.systems.school.managment.Data;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Seeds
{
    public static class SeedProducts
    {
        public static async Task Run()
        {
            await Seed();
        }

        private static async Task Seed()
        {
            var db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            if (await db.Products.FirstOrDefaultAsync() is null)
            {
                List<Product> products = new List<Product>
                {
                  new Product { Price =1450  ,Description ="Uniforme completo (calça ou saia e camisa)"},
                new Product { Price =2720  ,Description ="Uniforme completo da escola secundária"},
                new Product { Price =1300  ,Description ="Equipamento de ginástica"},
                new Product { Price =2900  ,Description = "Fato de treino "},
                new Product { Price =1600  ,Description = "Camisola"},
                new Product { Price =1350  ,Description = "Plover"},
                new Product { Price = 320  , Description = "Meias"},

                };

                await db.AddRangeAsync(products);
                await db.SaveChangesAsync();

            }


        }
    }
}









