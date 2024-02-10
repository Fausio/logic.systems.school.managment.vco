using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace logic.systems.school.managment.Services
{
    public class SempleEntityService : ISempleEntityService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

        public async Task<SimpleEntity> GetById(int Id)
        {
            try
            {
                return await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetByType(string type)
        {
            try
            {
                return await db.SimpleEntitys.Where(x => x.Type == type).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetByTypeOrderByDescription(string type)
        {
            try
            {
                var result = await GetByType(type);
                return result.OrderBy(x => x.Description).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetByTypeOrderById(string type)
        {
            try
            {
                var result = await GetByType(type);
                return result.OrderBy(x => x.Id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SimpleEntity>> GetGetSchoolClassRoomsBySchoolLevelId(int schoolLevelId)
        {
            var results = await GetByTypeOrderById("SchoolClassRoom");
            var SchoolLevel = await db.SimpleEntitys.FirstOrDefaultAsync(x => x.Id == schoolLevelId);



            if ("12ª classe" == SchoolLevel.Description)
            {
                return results.Where(x => x.Description.Contains("Grupo")).ToList();
            }
            else
            {
                return results.Where(x => x.Description.Contains("Turma")).ToList();
            }
        }

        public async Task<List<SimpleEntity>> GetSubjectsBySchoolLevel(int SchoolLevelId)
        {
            var levelResult = await GetById(SchoolLevelId);
            var result = await GetSubjects(levelResult.Description);
            return result;
        }

        private async Task<List<SimpleEntity>> GetSubjects(string Subject)
        {

            var result = new List<SimpleEntity>();

            switch (Subject)
            {

                #region 1 a 3
                case "1ª classe":
                case "2ª classe":
                case "3ª classe":

                    List<string> Subjects_1_3 = new List<string>
                        {
                            "Português",
                            "Matemática",
                            "Inglês",
                            "Francês",
                            "Educação física"
                        };


                    result.AddRange(await db.SimpleEntitys.Where(x => Subjects_1_3.Contains(x.Description)).ToListAsync());


                    break;
                #endregion




                #region 4
                case "4ª classe":

                    List<string> Subjects_4 = new List<string>
                        {
                       "Português",
    "Matemática",
    "Inglês",
    "Francês",
    "Ciências sociais",
    "Ciências naturais",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Educação física"
                        };

                    result.AddRange(await db.SimpleEntitys.Where(x => Subjects_4.Contains(x.Description)).ToListAsync());

                    break;
                #endregion






                #region 5 a 6
                case "5ª classe":
                case "6ª classe":

                    List<string> Subjects_5_6 = new List<string>
                        {
    "Português",
    "Matemática",
    "Inglês",
    "Francês",
    "Ciências sociais",
    "Ciências naturais",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Educação visual & ofício",
    "Educação física"
                        };

                    result.AddRange(await db.SimpleEntitys.Where(x => Subjects_5_6.Contains(x.Description)).ToListAsync());

                    break;
                #endregion

















                #region 7
                case "7ª classe":

                    List<string> Subjects_7 = new List<string>
                        {
"Português",
    "Matemática",
    "Inglês",
    "Francês",
    "História",
    "Geografia",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Biologia",
    "Noções de contabilidade",
    "Agropecuária",
    "Educação visual",
    "Educação física"
                        };

                    result.AddRange(await db.SimpleEntitys.Where(x => Subjects_7.Contains(x.Description)).ToListAsync());

                    break;
                #endregion















                #region 8, 9 e 10
                case "8ª classe":
                case "9ª classe":
                case "10ª classe":

                    List<string> Subjects_8_9_10 = new List<string>
                        {
    "Português",
    "Matemática",
    "Biologia",
    "Química",
    "Física",
    "Geografia",
    "História",
    "Educação visual",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Agropecuária",
    "Noções de contabilidade",
    "Francês",
    "Inglês",
    "Educação física"
                        };
                    result.AddRange(await db.SimpleEntitys.Where(x => Subjects_8_9_10.Contains(x.Description)).ToListAsync());

                    break;
                #endregion










                #region 11
                case "11ª classe":

                    List<string> Subjects_11 = new List<string>
                        {
    "Português",
    "Matemática",
    "Inglês",
    "Francês",
    "Geografia",
    "Biologia",
    "Química",
    "Filosofia",
    "TIC's (Tecnologias da Informação e Comunicação)",
    "Física",
    "Desenho geométrico descritivo",
    "Educação física"
                        };

                    result.AddRange(await db.SimpleEntitys.Where(x => Subjects_11.Contains(x.Description)).ToListAsync());

                    break;
                #endregion




                default:
                    Console.WriteLine("Class");
                    break;
            }


            return result;
        }
    }
}
