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

            if (await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == "SchoolClassRoom") is null)
            {
                List<SimpleEntity> schoolLevels = new List<SimpleEntity>
                {
                new SimpleEntity { Type = "SchoolClassRoom", Description = "Turma A" },
                new SimpleEntity { Type = "SchoolClassRoom", Description = "Turma B" },
                new SimpleEntity { Type = "SchoolClassRoom", Description = "Turma C" },
                };

                await db.AddRangeAsync(schoolLevels);
                await db.SaveChangesAsync();

            }

            if (await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Type == "Subject") is null)
            {
                List<SimpleEntity> schoolLevels = new List<SimpleEntity>
                {
                new SimpleEntity { Type = "Subject", Description = "Português" },
                new SimpleEntity { Type = "Subject", Description = "Matemática" },
                new SimpleEntity { Type = "Subject", Description = "Inglês" },
                new SimpleEntity { Type = "Subject", Description = "Francês" },
                new SimpleEntity { Type = "Subject", Description = "Educação física" },
                new SimpleEntity { Type = "Subject", Description = "Ciências sociais" },
                new SimpleEntity { Type = "Subject", Description = "Ciências naturais" },
                new SimpleEntity { Type = "Subject", Description = "TIC's (Tecnologias da Informação e Comunicação)" },
                new SimpleEntity { Type = "Subject", Description = "Educação visual & ofício" },
                new SimpleEntity { Type = "Subject", Description = "História" },
                new SimpleEntity { Type = "Subject", Description = "Geografia" },
                new SimpleEntity { Type = "Subject", Description = "Biologia" },
                new SimpleEntity { Type = "Subject", Description = "Noções de contabilidade" },
                new SimpleEntity { Type = "Subject", Description = "Agropecuária" },
                new SimpleEntity { Type = "Subject", Description = "Educação visual" },
                new SimpleEntity { Type = "Subject", Description = "Química" },
                new SimpleEntity { Type = "Subject", Description = "Física" },
                new SimpleEntity { Type = "Subject", Description = "Filosofia" },
                new SimpleEntity { Type = "Subject", Description = "Desenho geométrico descritivo" }

                };

                await db.AddRangeAsync(schoolLevels);
                await db.SaveChangesAsync();

            }

        }
    }
}
