using logic.systems.school.managment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Controllers
{
    public class CommonController : Controller
    {

        private readonly ApplicationDbContext db;
        public CommonController()
        {
            db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        }

        [HttpGet]
        public async Task<IActionResult> GetProfessorConfigs(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId não fornecido.");
            }

            var professorConfigs = await db.ProfessorConfig
                .Include(x => x.ClassLevel)
                 .Include(x => x.ClassRoom)
                 .Include(x => x.Subject)
                .Where(pc => pc.UserId == userId)
                .ToListAsync();

            return Json(professorConfigs);
        }
    }
}
