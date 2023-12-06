using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class SempleEntityService : ISempleEntityService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<List<SimpleEntity>> GetByType(string type)
        {
            try
            {
               return  await db.SimpleEntitys.Where(x => x.type == type).ToListAsync(); 
            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}
